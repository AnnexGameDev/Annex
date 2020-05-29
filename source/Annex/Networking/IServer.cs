using Annex.Networking.Packets;

namespace Annex.Networking
{
    public interface IServer
    {
        void SendPacket(object client, int packetID, OutgoingPacket packet);
    }
}
