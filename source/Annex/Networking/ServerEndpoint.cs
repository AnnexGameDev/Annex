using Annex.Networking.Configuration;
using Annex.Networking.Packets;
using System;
using System.Collections.Generic;

namespace Annex.Networking
{
    public abstract class ServerEndpoint<T> : SocketEndpoint<T>, IServer where T : Connection, new()
    {
        public const string NetworkEventID = "process-network-server";
        public readonly ServerConfiguration Configuration;
        private readonly ConnectionList<T> _connections;

        public int NumConnections => this._connections.Size;

        public ServerEndpoint(ServerConfiguration config) {
            this.Configuration = config;
            this._connections = new ConnectionList<T>();
        }

        public override T CreateConnectionIfNotExistsAndGet(object baseConnection) {
            return this._connections.CreateIfNotExistsAndGet(baseConnection, this);
        }

        public T? GetConnection(int id) {
            return this._connections.Get(id);
        }

        public T? GetConnection(object baseConnection) {
            return this._connections.Get(baseConnection);
        }

        public IEnumerable<T> GetConnectionsWhere(Func<T, bool> cmp) {
            return this._connections.Where(cmp);
        }

        public abstract void DisconnectClient(int id);

        protected void RemoveClient(int id) {
            this._connections.RemoveAt(id);
        }

        private protected abstract void SendPacket(T client, int packetID, OutgoingPacket packet);

        public void SendPacket(object client, int packetID, OutgoingPacket packet) {
            this.SendPacket(client as T, packetID, packet);
        }

        public override void Destroy() {
            this._connections.Clear();
        }
    }
}
