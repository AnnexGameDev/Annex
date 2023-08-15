namespace Annex_Old.Networking.Packets
{
    public interface IPacketReadable
    {
        void ReadFrom(IncomingPacket packet);
    }

    public interface IPacketWritable
    {
        void WriteTo(OutgoingPacket packet);
    }
}
