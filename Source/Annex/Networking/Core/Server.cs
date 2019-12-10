using Annex.Events;
using Annex.Networking.Configuration;
using Annex.Networking.Packets;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace Annex.Networking.Core
{
    public class Server<T> : Networking.Server<T>, IServer where T : Connection, new()
    {
        private abstract class CoreServer
        {
            public delegate void ReceiveHandler(object baseConnection, byte[] data);
            public event ReceiveHandler OnReceive;

            public abstract void Start();
            public abstract void Destroy();
            public abstract void SendPacket(object baseConnection, int packetID, OutgoingPacket packet);

            private protected void PacketReceived(object baseConnection, byte[] data) {
                this.OnReceive?.Invoke(baseConnection, data);
            }
        }

        private class TcpServer : CoreServer
        {
            private class TcpClientConnection
            {
                private readonly Socket _baseSocket;
                private byte[] _receiveBuffer;
                private byte[] _processingBuffer;

                public TcpClientConnection(Socket baseSocket, TcpServer tcpServer) {
                    this._baseSocket = baseSocket;
                    this._receiveBuffer = new byte[this._baseSocket.ReceiveBufferSize];
                    this._processingBuffer = new byte[0];

                    this._baseSocket.BeginReceive(this._receiveBuffer, 0, this._receiveBuffer.Length, SocketFlags.None, ReceiveCallback, tcpServer);
                }

                private void ReceiveCallback(IAsyncResult ar) {
                    if (!this._baseSocket.Connected) {
                        return;
                    }

                    this._baseSocket.BeginReceive(this._receiveBuffer, 0, this._receiveBuffer.Length, SocketFlags.None, ReceiveCallback, ar.AsyncState);

                    var server = (TcpServer)ar.AsyncState;

                    int lengthOfIncomingData = 0;

                    try {
                        lengthOfIncomingData = this._baseSocket.EndReceive(ar);
                    } catch (SocketException e) {
                        switch (e.SocketErrorCode) {
                            case SocketError.ConnectionReset:
                                this._baseSocket.Disconnect(false);
                                return;
                        }
                    }

                    // If length of incoming data is 0, it means we disconnected.
                    if (lengthOfIncomingData == 0) {
                        this._baseSocket.Disconnect(false);
                        return;
                    }

                    // Shift everything over to the processing buffer.
                    var newProcessingBuffer = new byte[this._receiveBuffer.Length + lengthOfIncomingData];
                    Array.Copy(this._receiveBuffer, 0, newProcessingBuffer, 0, this._receiveBuffer.Length);
                    Array.Copy(this._receiveBuffer, 0, newProcessingBuffer, this._processingBuffer.Length, lengthOfIncomingData);
                    this._receiveBuffer = new byte[this._baseSocket.ReceiveBufferSize];
                    this._processingBuffer = newProcessingBuffer;

                    if (this._processingBuffer.Length < 4) {
                        return;
                    }

                    int packetLength = BitConverter.ToInt32(this._processingBuffer, 0);

                    if (this._processingBuffer.Length >= packetLength + 4) {
                        var packet = new byte[packetLength];
                        Array.Copy(this._processingBuffer, 4, packet, 0, packetLength);
                        server.PacketReceived(this, packet);

                        int newProcessingBufferSize = this._receiveBuffer.Length - (packetLength + 4);

                        newProcessingBuffer = new byte[newProcessingBufferSize];
                        Array.Copy(this._receiveBuffer, packetLength + 4, newProcessingBuffer, 0, newProcessingBufferSize);
                        this._processingBuffer = newProcessingBuffer;
                    }
                }

                internal void SendPacket(int packetID, OutgoingPacket packet) {
                    var rawData = packet.GetBytes();
                    var fullData = new byte[8 + rawData.Length];

                    var packetIDData = BitConverter.GetBytes(packetID);
                    var packetLengthData = BitConverter.GetBytes(4 + rawData.Length);

                    Array.Copy(packetLengthData, 0, fullData, 0, 4);
                    Array.Copy(packetIDData, 0, fullData, 4, 4);
                    Array.Copy(rawData, 0, fullData, 8, rawData.Length);

                    this._baseSocket.BeginSend(fullData, 0, fullData.Length, SocketFlags.None, SendCallback, null);
                }

                private void SendCallback(IAsyncResult ar) {
                    this._baseSocket.EndSend(ar);
                }
            }

            private ServerConfiguration _config;
            private Socket _socket;

            public TcpServer(ServerConfiguration config) {
                this._config = config;
            }

            public override void Start() {
                this._socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                this._socket.Bind(new IPEndPoint(IPAddress.Loopback, this._config.Port));
                this._socket.Listen(5);
                this._socket.BeginAccept(AcceptCallback, null);
            }

            private void AcceptCallback(IAsyncResult ar) {
                this._socket.Listen(5);
                this._socket.BeginAccept(AcceptCallback, null);

                var clientSocket = this._socket.EndAccept(ar);
                new TcpClientConnection(clientSocket, this);
            }

            public override void Destroy() {
                this._socket.Disconnect(false);
            }

            public override void SendPacket(object baseConnection, int packetID, OutgoingPacket packet) {
                var tcpClientConnection = (TcpClientConnection)baseConnection;
                tcpClientConnection.SendPacket(packetID, packet);
            }
        }

        private class UdpServer : CoreServer
        {
            private ServerConfiguration _config;
            private UdpClient _udpClient;

            public UdpServer(ServerConfiguration configuration) {
                this._config = new ServerConfiguration();
            }

            public override void Start() {
                this._udpClient = new UdpClient(this._config.Port, AddressFamily.InterNetwork);
                this._udpClient.BeginReceive(this.ReceiveCallback, null);
            }

            public override void Destroy() {
                this._udpClient.Close();
            }

            private void ReceiveCallback(IAsyncResult ar) {
                // source for this line: https://stackoverflow.com/questions/7266101/receive-messages-continuously-using-udpclient
                this._udpClient.BeginReceive(ReceiveCallback, null);

                var endpoint = new IPEndPoint(IPAddress.Any, 0);
                byte[] data = this._udpClient.EndReceive(ar, ref endpoint);

                this.PacketReceived(endpoint, data);
            }

            public override void SendPacket(object baseConnection, int packetID, OutgoingPacket packet) {
                var data = packet.GetBytes();
                this._udpClient.Send(packet.GetBytes(), data.Length, (IPEndPoint)baseConnection);
            }
        }

        private readonly CoreServer _server;
        private Queue<(int id, byte[] data)> _messagesToProcess;

        public Server(ServerConfiguration config) : base(config) {
            this._messagesToProcess = new Queue<(int id, byte[] data)>();

            if (config.Protocol == Protocol.TCP) {
                this._server = new TcpServer(config);
            }
            if (config.Protocol == Protocol.UDP) {
                this._server = new UdpServer(config);
            }
            this._server.OnReceive += OnReceive;
        }

        private void OnReceive(object baseConnection, byte[] data) {
            T connection;
            if (!this.Connections.Exists(baseConnection)) {
                connection = new T();
                int id = this.Connections.GetFreeID();
                connection.SetID(id);
                connection.SetBaseConnection(baseConnection);
                connection.SetEndpoint(this);
                this.Connections.Add(connection);
            } else {
                connection = this.Connections.Get(baseConnection);
            }

            lock (this._messagesToProcess) {
                this._messagesToProcess.Enqueue(((int)connection.ID, data));
            }
        }

        public override void Destroy() {
            this._server.Destroy();
        }

        public override void SendPacket(T client, int packetID, OutgoingPacket packet) {
            this._server.SendPacket(client.BaseConnection, packetID, packet);
        }

        public override void Start() {
            Console.WriteLine($"Creating server: {this.Configuration}");
            this._server.Start();
            EventManager.Singleton.AddEvent(PriorityType.NETWORK, this.ProcessQueue, 0, 0, "server-client-core-process-queue");
        }

        private ControlEvent ProcessQueue() {
            lock (this._messagesToProcess) {
                while (this._messagesToProcess.Count != 0) {
                    (int id, byte[] data) = this._messagesToProcess.Dequeue();
                    var connection = this.Connections.Get(id);
                    using var packet = new IncomingPacket(data);
                    this.PacketHandler.HandlePacket(connection, packet);
                }
            }
            return ControlEvent.NONE;
        }
    }
}
