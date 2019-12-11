using Annex.Networking.Configuration;
using Annex.Networking.Packets;

namespace Annex.Networking
{
    public abstract class Server<T> : SocketEndpoint<T>, IServer where T : Connection
    {
        public readonly ServerConfiguration Configuration;

        public const string NetworkEventID = "process-network-server";

        public Server(ServerConfiguration config) {
            this.Configuration = config;
        }

        public abstract void SendPacket(T client, int packetID, OutgoingPacket packet);

        public void SendPacket(object client, int packetID, OutgoingPacket packet) {
            this.SendPacket(client as T, packetID, packet);
        }
    }
}
