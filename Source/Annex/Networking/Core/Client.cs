using Annex.Networking.Configuration;
using Annex.Networking.Packets;

namespace Annex.Networking.Core
{
    public class Client<T> : Networking.Client<T>, IClient where T : Connection
    {
        public Client(ClientConfiguration config) : base(config) {
        }

        public override void Destroy() {
        }

        public override void SendPacket(int packetID, OutgoingPacket o) {
        }

        public override void Start() {
        }
    }
}
