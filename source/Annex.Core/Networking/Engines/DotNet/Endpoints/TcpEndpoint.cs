using Annex.Core.Networking.Packets;
using Scaffold.Logging;
using System.Collections.Concurrent;
using System.Net.Sockets;

namespace Annex.Core.Networking.Engines.DotNet.Endpoints;

internal abstract class TcpEndpoint : IEndpoint
{
    protected readonly EndpointConfiguration Config;
    protected readonly Socket Socket;

    protected ConcurrentDictionary<Guid, TcpConnection> Connections = new();
    protected bool Disposed { get; private set; }

    public TcpEndpoint(EndpointConfiguration config) {
        Config = config;
        Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
        {
            NoDelay = true
        };
    }

    protected virtual void HandleNewConnection(TcpConnection connection) {

        if (Connections.ContainsKey(connection.Id))
        {
            Log.Error($"Connection {connection} is already registered");
            return;
        }
        Log.Normal($"New connection accepted: {connection}");

        connection.OnConnectionStateChanged += Connection_OnConnectionStateChanged;

        Connections.TryAdd(connection.Id, connection);
        connection.ListenForIncomingPackets();
    }

    private void Connection_OnConnectionStateChanged(object? sender, Connections.ConnectionState connectionState) {
        var connection = (TcpConnection)sender!;
        if (connectionState == Networking.Connections.ConnectionState.Disconnected)
        {
            HandleDisconnectedConnection(connection);
        }
    }

    protected virtual void HandleDisconnectedConnection(TcpConnection connection) {
        if (!Connections.ContainsKey(connection.Id))
        {
            Log.Error($"Connection {connection} is not registered");
            return;
        }

        connection.OnConnectionStateChanged -= Connection_OnConnectionStateChanged;
        Connections.TryRemove(connection.Id, out _);
        connection.Dispose();
    }

    protected void SendTo(TcpConnection connection, OutgoingPacket packet) {
        if (!Connections.ContainsKey(connection.Id))
        {
            Log.Error($"Connection {connection} is not registered");
            return;
        }
        connection.Send(packet);
    }

    protected virtual void Dispose(bool disposing) {
        if (!Disposed)
        {
            if (disposing)
            {
                foreach (var connection in Connections.Values)
                {
                    HandleDisconnectedConnection(connection);
                }
                Socket.Dispose();
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            Disposed = true;
        }
    }

    // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    // ~TcpEndpoint()
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
