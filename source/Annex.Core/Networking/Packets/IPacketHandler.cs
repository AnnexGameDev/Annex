namespace Annex.Core.Networking.Packets
{
    public interface IPacketHandler
    {
        void Handle(Connection connection, IncomingPacket packet);
    }
}
