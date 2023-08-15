namespace Annex.Core.Networking.Connections;

public interface IConnection : IDisposable
{
    event EventHandler<ConnectionState>? OnConnectionStateChanged;

    Guid Id { get; }
    ConnectionState State { get; }

    void Destroy(string reason, Exception? exception = null);
}
