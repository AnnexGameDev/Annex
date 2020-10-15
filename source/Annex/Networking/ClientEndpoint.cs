using Annex.Networking.Configuration;
using Annex.Networking.Packets;

namespace Annex.Networking
{
    public abstract class ClientEndpoint<T> : SocketEndpoint<T>, IClient where T : Connection, new()
    {
        public readonly ClientConfiguration Configuration;
        public const string NetworkEventID = "process-network-client";

        private T? _connection;
        public T? Connection => this._connection;

        public ClientEndpoint(ClientConfiguration config) {
            this.Configuration = config;
        }

        public override T CreateConnectionIfNotExistsAndGet(object baseConnection) {
            Debug.Assert(this._connection == null, "Tried to create new connection");
            this._connection = new T();
            this._connection.SetBaseConnection(baseConnection);
            this._connection.SetID(0);
            this._connection.SetEndpoint(this);

            return this._connection;
        }

        public override void Destroy() {
            this._connection = null;
        }

        public abstract void SendPacket(int packetID, OutgoingPacket o);
    }
}
