using Annex.Core.Networking.Connections;

namespace Annex.Core.Networking.Packets
{
    public interface IPacketHandler
    {
        int PacketId { get; }
        void Handle(IConnection connection, IncomingPacket packet);
    }
}
