using Annex_Old.Core.Networking.Connections;

namespace Annex_Old.Core.Networking.Packets
{
    public interface IPacketHandler
    {
        void Handle(Connection connection, IncomingPacket packet);
    }
}
