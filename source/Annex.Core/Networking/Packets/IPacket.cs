namespace Annex.Core.Networking.Packets
{
    public interface IPacket : IDisposable
    {
        public const int ResponsePacketId = -1;
    }
}
