using Scaffold.Logging;

namespace Annex.Core.Networking.Connections
{
    public abstract class Connection : IConnection
    {
        private bool disposedValue;
        public Guid Id { get; } = Guid.NewGuid();

        public event EventHandler<ConnectionState>? OnConnectionStateChanged;
        private ConnectionState _state = ConnectionState.Unknown;
        public ConnectionState State
        {
            get => this._state;
            protected set
            {
                this._state = value;
                this.OnConnectionStateChanged?.Invoke(this, value);
            }
        }

        public override int GetHashCode() {
            return this.Id.GetHashCode();
        }

        public override bool Equals(object? obj) {
            if (obj is not IConnection connection) {
                return false;
            }
            return connection.Id.Equals(this.Id);
        }

        protected virtual void Dispose(bool disposing) {
            if (!disposedValue) {
                if (disposing) {
                    this.Destroy("Disposing");
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~Connection()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose() {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public virtual void Destroy(string reason, Exception? exception = null) {
            Log.Trace(LogSeverity.Normal, $"Disconnecting client {this.Id}: {reason}", exception);
        }
    }
}
