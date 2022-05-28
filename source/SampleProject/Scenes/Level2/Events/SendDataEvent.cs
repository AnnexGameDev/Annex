using Annex_Old.Core.Events;
using Annex_Old.Core.Networking;
using Annex_Old.Core.Networking.Packets;
using System;

namespace SampleProject.Scenes.Level2.Events
{
    internal class SendDataEvent : Event, IDisposable
    {
        private readonly IClientEndpoint _client;

        public SendDataEvent(INetworkingEngine networkingEngine) : base(1000, 0) {
            this._client = networkingEngine.CreateClient(new EndpointConfiguration());
            this._client.Start();
        }

        public void Dispose() {
            this._client.Dispose();
        }

        protected override void Run() {

            string data = Guid.NewGuid().ToString();
            using var packet = new OutgoingPacket((int)PacketId.SimpleMessage);
            packet.Write(data);

            Console.WriteLine($"Sending: {data}");
            this._client.Send(packet);
        }
    }
}
