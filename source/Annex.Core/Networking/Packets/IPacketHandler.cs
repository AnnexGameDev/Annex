using Annex.Core.Networking.Connections;

namespace Annex.Core.Networking.Packets
{
    public interface IPacketHandler
    {
        void Handle(IConnection connection, IncomingPacket packet);
    }
}
