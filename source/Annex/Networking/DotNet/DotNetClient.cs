using Annex.Events;
using Annex.Networking.Configuration;
using Annex.Networking.Packets;
using System;

namespace Annex.Networking.DotNet
{
    public class DotNetClient<T> : ClientEndpoint<T>, IClient where T : Connection, new()
    {
        private readonly CoreSocket _client;
        private readonly MessageQueue<T> _messageQueue;

        public DotNetClient(ClientConfiguration config) : base(config) {
            this._messageQueue = new MessageQueue<T>(this);

            if (config.Method == TransmissionType.UnreliableUnordered) {
                this._client = new Udp.Client(config);
            }
            if (config.Method == TransmissionType.ReliableOrdered) {
                this._client = new Tcp.Client(config);
            }

            this._client.OnReceive += this._messageQueue.OnReceive;
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
            ServiceProvider.EventManager.AddEvent(PriorityType.NETWORK, this._messageQueue.ProcessQueue, 0, 0, "client-core-process-queue");
        }
    }
}
