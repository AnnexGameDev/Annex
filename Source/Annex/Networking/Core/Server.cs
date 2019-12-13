using Annex.Events;
using Annex.Networking.Configuration;
using Annex.Networking.Packets;
using System;

namespace Annex.Networking.Core
{
    public class Server<T> : Networking.Server<T>, IServer where T : Connection, new()
    {
        private readonly CoreSocket _server;
        private readonly MessageQueue<T> _messageQueue;

        public Server(ServerConfiguration config) : base(config) {
            this._messageQueue = new MessageQueue<T>(this);

            if (config.Method == TransmissionType.ReliableOrdered) {
                this._server = new Tcp.Server(config);
            }
            if (config.Method == TransmissionType.UnreliableUnordered) {
                this._server = new Udp.Server(config);
            }
            this._server.OnReceive += this._messageQueue.OnReceive;
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
            EventManager.Singleton.AddEvent(PriorityType.NETWORK, this._messageQueue.ProcessQueue, 0, 0, "server-core-process-queue");
        }
    }
}
