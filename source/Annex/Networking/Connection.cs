namespace Annex.Networking
{
    public abstract class Connection
    {
        public int? ID { get; private set; }
        public object? Endpoint { get; private set; }
        public object? BaseConnection { get; private set; }
        public ConnectionState State { get; private set; }

        public delegate void OnChangeStateHandle(ConnectionState newState);
        public event OnChangeStateHandle? OnChangeConnectionState;

        public void SetState(ConnectionState state) {
            this.State = state;
            this.OnChangeConnectionState?.Invoke(state);
        }

        public void SetBaseConnection(object baseConnection) {
            Debug.Assert(this.BaseConnection == null, "Base connection has already been set");
            this.BaseConnection = baseConnection;
        }

        public void SetID(int id) {
            Debug.Assert(this.ID == null, "ID has already been set");
            this.ID = id;
        }

        public void SetEndpoint(object endpoint) {
            Debug.Assert(this.Endpoint == null, "Endpoint has already been set");
            this.Endpoint = endpoint;
        }

        public void Destroy() {
            this.ID = null;
            this.Endpoint = null;
            this.BaseConnection = null;
            this.SetState(ConnectionState.Unknown);
        }
    }
}
