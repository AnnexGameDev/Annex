namespace Annex_Old.Networking.Packets
{
    public interface IIncomingPacketHandler<T> where T : Connection
    {
        void Handle(T connection, IncomingPacket packet);
    }
}
