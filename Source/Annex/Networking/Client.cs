using Annex.Networking.Configuration;
using Annex.Networking.Packets;

namespace Annex.Networking
{
    public abstract class Client<T> : SocketEndpoint<T>, IClient where T : Connection, new()
    {
        public readonly ClientConfiguration Configuration;
        public const string NetworkEventID = "process-network-client";

        public Client(ClientConfiguration config) {
            this.Configuration = config;
        }

        public abstract void SendPacket(int packetID, OutgoingPacket o);
    }
}
