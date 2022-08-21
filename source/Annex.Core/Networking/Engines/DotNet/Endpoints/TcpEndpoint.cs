using Annex.Core.Networking.Packets;
using Scaffold.Collections.Generic;
using Scaffold.Logging;
using System.Net.Sockets;

namespace Annex.Core.Networking.Engines.DotNet.Endpoints;

internal abstract class TcpEndpoint : IEndpoint
{
    protected readonly EndpointConfiguration Config;
    protected readonly Socket Socket;

    protected ConcurrentHashSet<TcpConnection> Connections = new();
    protected bool Disposed { get; private set; }

    public TcpEndpoint(EndpointConfiguration config) {
        this.Config = config;
        this.Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    }

    protected virtual void HandleNewConnection(TcpConnection connection) {

        if (this.Connections.Contains(connection)) {
            Log.Trace(LogSeverity.Error, $"Connection {connection} is already registered");
            return;
        }

        connection.OnConnectionStateChanged += Connection_OnConnectionStateChanged;

        this.Connections.Add(connection);
        connection.ListenForIncomingPackets();
    }

    private void Connection_OnConnectionStateChanged(object? sender, Connections.ConnectionState connectionState) {
        var connection = (TcpConnection)sender!;
        if (connectionState == Networking.Connections.ConnectionState.Disconnected) {
            HandleDisconnectedConnection(connection);
        }
    }

    protected virtual void HandleDisconnectedConnection(TcpConnection connection) {
        if (!this.Connections.Contains(connection)) {
            Log.Trace(LogSeverity.Error, $"Connection {connection} is not registered");
            return;
        }

        connection.OnConnectionStateChanged -= Connection_OnConnectionStateChanged;
        this.Connections.Remove(connection);
        connection.Dispose();
    }

    protected void SendTo(TcpConnection connection, OutgoingPacket packet) {
        if (!this.Connections.Contains(connection)) {
            Log.Trace(LogSeverity.Error, $"Connection {connection} is not registered");
            return;
        }
        connection.SendOutgoingPacket(packet);
    }

    protected virtual void Dispose(bool disposing) {
        if (!Disposed) {
            if (disposing) {
                foreach (var connection in this.Connections) {
                    this.HandleDisconnectedConnection(connection);
                }

                this.Socket.Dispose();
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
