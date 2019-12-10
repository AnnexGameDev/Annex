using Annex.Networking.Packets;

namespace Annex.Networking
{
    public interface IClient
    {
        void SendPacket(int packetID, OutgoingPacket o);
    }
}
