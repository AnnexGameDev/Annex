using Annex.Core.Networking;
using Annex.Core.Networking.Connections;
using Annex.Core.Networking.Packets;
using Annex.Core.Scenes.Elements;
using System;
using System.Threading.Tasks;

namespace SampleProject.Scenes.Level4
{
    internal class Level4Scene : Scene
    {
        private readonly IServerEndpoint _server;
        private readonly IClientEndpoint _client;

        public Level4Scene(INetworkingEngine networkingEngine) {
            var config = new EndpointConfiguration();
            _server = networkingEngine.CreateServer(config);
            _server.Start();

            _client = networkingEngine.CreateClient(config);
            _client.Start();

            for (int i = 0; i < 10; i++)
            {
                int val = i;
                Task.Run(async () => {
                    try
                    {
                        using var request = CreateDataRequestPacket(val);
                        Console.WriteLine($"[Client] {val} -> {request.RequestId}");
                        using var response = await _client.SendAsync(request);
                        await Task.Delay(1000);
                        Console.WriteLine($"[Client] Got {response.ReadInt()} -> {response.OriginalRequestId}");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                });
            }
        }

        private OutgoingPacket CreateDataRequestPacket(int id) {
            var outgoingPacket = new OutgoingPacket(SimpleRequestPacketHandler.PacketId);
            outgoingPacket.Write(id);
            return outgoingPacket;
        }
    }

    public class SimpleRequestPacketHandler : IPacketHandler
    {
        public const int PacketId = 4001;

        public int Id { get; } = PacketId;

        public async Task HandleAsync(IConnection connection, IncomingPacket packet) {
            var id = packet.ReadInt();

            Console.WriteLine($"[Server] {id} -> {packet.OriginalRequestId}");

            await Task.Delay(1000);
            using var response = new OutgoingPacket(packet);
            response.Write(id);

            connection.Send(response);
        }
    }
}
