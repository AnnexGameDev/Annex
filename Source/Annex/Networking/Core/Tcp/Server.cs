using Annex.Networking.Configuration;
using Annex.Networking.Packets;
using System;
using System.Net;
using System.Net.Sockets;

namespace Annex.Networking.Core.Tcp
{
    public class Server : CoreSocket
    {
        private class TcpClientConnection
        {
            private readonly Socket _baseSocket;
            private byte[] _receiveBuffer;
            private byte[] _processingBuffer;

            public TcpClientConnection(Socket baseSocket, Server tcpServer) {
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

                var server = (Server)ar.AsyncState;

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

        private Socket _socket;

        public Server(ServerConfiguration config) : base(config) {
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
}
