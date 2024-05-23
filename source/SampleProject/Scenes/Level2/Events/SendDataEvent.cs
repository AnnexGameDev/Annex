using Annex.Core.Events;
using Annex.Core.Networking;
using Annex.Core.Networking.Packets;
using System;
using System.Threading.Tasks;

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


        protected override Task RunAsync() {

            string data = Guid.NewGuid().ToString();
            using var packet = new OutgoingPacket((int)PacketId.SimpleMessage);
            packet.Write(data);

            Console.WriteLine($"Sending: {data}");
            return this._client.SendAsync(packet);
        }
    }
}
