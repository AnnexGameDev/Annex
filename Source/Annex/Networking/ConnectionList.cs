using System.Collections.Generic;

namespace Annex.Networking
{
    public class ConnectionList<T> where T : Connection, new()
    {
        private readonly List<T> _connections;
        private readonly Dictionary<object, int> _connectionMap;

        public ConnectionList() {
            this._connections = new List<T>();
            this._connectionMap = new Dictionary<object, int>();
        }

        public int GetFreeID() {
            int id;
            for (id = 0; id < this._connections.Count; id++) {
                if (this._connections[id] == null) {
                    break;
                }
            }
            return id;
        }

        private void Add(T connection) {
            Debug.Assert(connection.ID >= 0);
            Debug.Assert(connection.ID <= this._connections.Count);

            // If we're overwriting an existing index, it needs to be false.
            Debug.Assert(connection.ID < this._connections.Count ? this._connections[(int)connection.ID] == null : true);

            if (this._connections.Count == connection.ID) {
                this._connections.Add(null);
            }

            this._connections[(int)connection.ID] = connection;
            this._connectionMap[connection.BaseConnection] = (int)connection.ID;
        }

        public T CreateIfNotExistsAndGet(object baseConnection, SocketEndpoint<T> endpoint) {
            if (!this.Exists(baseConnection)) {
                var connection = new T();
                connection.SetBaseConnection(baseConnection);
                connection.SetID(this.GetFreeID());
                connection.SetEndpoint(endpoint);
                this.Add(connection);
            }
            return this.Get(baseConnection);
        }

        public T Get(int index) {
            Debug.Assert(index >= 0);
            Debug.Assert(index < this._connections.Count);

            return this._connections[index];
        }

        public T Get(object baseConnection) {
            Debug.Assert(Exists(baseConnection));
            return this.Get(this._connectionMap[baseConnection]);
        }

        public bool Exists(object baseConnection) {
            return this._connectionMap.ContainsKey(baseConnection);
        }
    }
}
