using Annex.Core.Networking.Packets;

namespace Annex.Core.Networking.Connections;

public interface IConnection : IDisposable
{
    event EventHandler<ConnectionState>? OnConnectionStateChanged;

    Guid Id { get; }
    ConnectionState State { get; }

    void Send(OutgoingPacket packet);

    void Destroy(string reason, Exception? exception = null);
}
