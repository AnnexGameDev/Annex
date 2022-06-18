namespace Annex.Core.Networking.Packets
{
    public class PacketHandlerAttribute : Attribute
    {
        public int PacketId { get; }

        public PacketHandlerAttribute(object packetId) {
            try {
                this.PacketId = (int)packetId;
            }
            catch {
                throw;
            }
        }

        public PacketHandlerAttribute(int packetId) {
            this.PacketId = packetId;
        }
    }
}
