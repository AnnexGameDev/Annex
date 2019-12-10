using Annex.Networking.Packets;

namespace Annex.Networking
{
    public interface IServer
    {
        public abstract void SendPacket(object client, int packetID, OutgoingPacket packet);
    }
}
