using Annex.Events;
using Annex.Networking.Configuration;
using Annex.Networking.Packets;
using System;
using System.Collections.Generic;

namespace Annex.Networking.Core
{
    public class Server<T> : Networking.Server<T>, IServer where T : Connection, new()
    {
        private readonly CoreSocket _server;
        private Queue<(int id, byte[] data)> _messagesToProcess;

        public Server(ServerConfiguration config) : base(config) {
            this._messagesToProcess = new Queue<(int id, byte[] data)>();

            if (config.Protocol == Protocol.TCP) {
                this._server = new Tcp.Server(config);
            }
            if (config.Protocol == Protocol.UDP) {
                this._server = new Udp.Server(config);
            }
            this._server.OnReceive += OnReceive;
        }

        private void OnReceive(object baseConnection, byte[] data) {
            T connection;
            if (!this.Connections.Exists(baseConnection)) {
                connection = new T();
                connection.SetBaseConnection(baseConnection);
                connection.SetEndpoint(this);
                lock (this.Connections) {
                    int id = this.Connections.GetFreeID();
                    connection.SetID(id);
                    this.Connections.Add(connection);
                }
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
            EventManager.Singleton.AddEvent(PriorityType.NETWORK, this.ProcessQueue, 0, 0, "server-core-process-queue");
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
