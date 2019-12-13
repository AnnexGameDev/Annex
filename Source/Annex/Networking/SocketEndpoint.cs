namespace Annex.Networking
{
    public abstract class SocketEndpoint<T> where T : Connection, new()
    {
        public readonly ConnectionList<T> Connections;
        public readonly PacketHandler<T> PacketHandler;

        public SocketEndpoint() {
            this.Connections = new ConnectionList<T>();
            this.PacketHandler = new PacketHandler<T>();
        }

        public abstract void Start();
        public abstract void Destroy();
    }
}
