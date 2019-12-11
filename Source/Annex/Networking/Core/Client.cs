using Annex.Events;
using Annex.Networking.Configuration;
using Annex.Networking.Packets;
using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace Annex.Networking.Core
{
    public class Client<T> : Networking.Client<T>, IClient where T : Connection, new()
    {
        private class CoreTcpClient
        {
            private class TcpClientConnection
            {
                private readonly Socket _baseSocket;
                private byte[] _receiveBuffer;
                private byte[] _processingBuffer;
            }
        }

        private readonly CoreSocket _client;
        private Queue<(int id, byte[] data)> _messagesToProcess;

        public Client(ClientConfiguration config) : base(config) {
            this._messagesToProcess = new Queue<(int id, byte[] data)>();

            if (config.Protocol == Protocol.UDP) {
                this._client = new Udp.Client(config);
            }

            this._client.OnReceive += OnReceive;
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
            this._client.Destroy();
        }

        public override void SendPacket(int packetID, OutgoingPacket o) {
            this._client.SendPacket(null, packetID, o);
        }

        public override void Start() {
            Console.WriteLine($"Creating client: {this.Configuration}");
            this._client.Start();
            EventManager.Singleton.AddEvent(PriorityType.NETWORK, this.ProcessQueue, 0, 0, "client-core-process-queue");
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
