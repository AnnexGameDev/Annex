using Annex.Core.Networking;
using Annex.Core.Networking.Packets;
using Annex.Core.Scenes.Components;
using System;

namespace SampleProject.Scenes.Level2
{
    public class Level2 : Scene
    {
        private readonly IServerEndpoint _server;
        private readonly IClientEndpoint _client;

        public Level2(INetworkingEngine networkingEngine) {

            var config = new EndpointConfiguration();
            this._server = networkingEngine.CreateServer(config);
            this._client = networkingEngine.CreateClient(config);

            this._server.Start();
            this._client.Start();

            using var message = new OutgoingPacket(PacketId.SimpleMessage);
            message.Write("Hello world!");
            this._client.Send(message);
        }
    }

    public enum PacketId : int
    {
        SimpleMessage
    }

    [PacketHandler(PacketId.SimpleMessage)]
    public class SimpleMessagePacketHandler : IPacketHandler
    {
        public void Handle(Connection connection, IncomingPacket packet) {
            Console.WriteLine(packet.ReadString("msg"));
        }
    }
}
