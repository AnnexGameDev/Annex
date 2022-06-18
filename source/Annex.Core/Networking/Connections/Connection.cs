namespace Annex.Core.Networking.Connections
{
    public class Connection : IDisposable
    {
        private bool disposedValue;
        public Guid Id { get; } = Guid.NewGuid();

        public event EventHandler? ConnectionStateChanged;
        private ConnectionState _state = ConnectionState.Unknown;
        public ConnectionState State
        {
            get => this._state;
            protected set
            {
                this._state = value;
                this.ConnectionStateChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public override int GetHashCode() {
            return this.Id.GetHashCode();
        }

        public override bool Equals(object? obj) {
            if (obj is not Connection connection) {
                return false;
            }
            return connection.Id.Equals(this.Id);
        }

        protected virtual void Dispose(bool disposing) {
            if (!disposedValue) {
                if (disposing) {
                    // TODO: dispose managed state (managed objects)
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
    }
}
