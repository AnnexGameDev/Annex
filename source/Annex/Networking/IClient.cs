using Annex_Old.Networking.Packets;

namespace Annex_Old.Networking
{
    public interface IClient
    {
        void SendPacket(int packetID, OutgoingPacket o);
    }
}
