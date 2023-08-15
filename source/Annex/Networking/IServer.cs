using Annex_Old.Networking.Packets;

namespace Annex_Old.Networking
{
    public interface IServer
    {
        void SendPacket(object client, int packetID, OutgoingPacket packet);
    }
}
