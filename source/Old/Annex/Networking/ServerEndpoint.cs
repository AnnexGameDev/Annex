using Annex.Networking.Configuration;
using Annex.Networking.Packets;

namespace Annex.Networking
{
    public abstract class ServerEndpoint<T> : SocketEndpoint<T>, IServer where T : Connection, new()
    {
        public readonly ServerConfiguration Configuration;

        public const string NetworkEventID = "process-network-server";

        public ServerEndpoint(ServerConfiguration config) {
            this.Configuration = config;
        }

        private protected abstract void SendPacket(T client, int packetID, OutgoingPacket packet);

        public void SendPacket(object client, int packetID, OutgoingPacket packet) {
            this.SendPacket(client as T, packetID, packet);
        }
    }
}
