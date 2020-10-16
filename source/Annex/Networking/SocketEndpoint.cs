using Annex.Networking.Packets;

namespace Annex.Networking
{
    public abstract class SocketEndpoint<T> where T : Connection, new()
    {
        private readonly PacketHandler<T> PacketHandler;

        public SocketEndpoint() {
            this.PacketHandler = new PacketHandler<T>();
        }

        public abstract T CreateConnectionIfNotExistsAndGet(object baseConnection);

        public void HandlePacket(T connection, IncomingPacket packet) {
            this.PacketHandler.HandlePacket(connection, packet);
        }

        public void AddPacketHandler(int packetId, IIncomingPacketHandler<T> handler) {
            this.PacketHandler.AddPacketHandler(packetId, handler);
        }

        public abstract void Start();
        public abstract void Destroy();
    }
}
