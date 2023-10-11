using Annex.Core.Networking.Connections;
using Annex.Core.Networking.Packets;
using Scaffold.Logging;
using System.Collections.Concurrent;

namespace Annex.Core.Networking.Engines.DotNet.Endpoints;

internal class TcpClient : TcpEndpoint, IClientEndpoint
{
    private TcpClientConnection _connection;
    public IConnection Connection => _connection;

    private readonly IDictionary<string, TaskCompletionSource<IncomingPacket>> _responseListeners = new ConcurrentDictionary<string, TaskCompletionSource<IncomingPacket>>();

    public TcpClient(EndpointConfiguration config, IPacketHandlerService packetHandlerService) : base(config) {
        this._connection = new TcpClientConnection(this.Socket, packetHandlerService);

        this.Connection.OnConnectionStateChanged += Connection_OnConnectionStateChanged;
        packetHandlerService.OnResponseReceived += PacketHandlerService_OnResponseReceived;
    }

    private void PacketHandlerService_OnResponseReceived(object? sender, IncomingPacket packet) {
        if (!_responseListeners.ContainsKey(packet.OriginalRequestId))
        {
            Log.Trace(LogSeverity.Error, $"No response handler was registered for {packet.OriginalRequestId}");
            return;
        }
        _responseListeners[packet.OriginalRequestId].TrySetResult(packet);
    }

    private void Connection_OnConnectionStateChanged(object? sender, ConnectionState state) {

        Log.Trace(LogSeverity.Normal, $"Connection {this.Connection} state changed to: {state}");

        switch (state)
        {
            case ConnectionState.Connected:
                this.HandleNewConnection(this._connection);
                break;
            case ConnectionState.Disconnected:
                this.HandleDisconnectedConnection(this._connection);
                break;
        }
    }

    public void Send(OutgoingPacket packet) {
        this.SendTo(this._connection, packet);
    }

    public Task<IncomingPacket> SendAsync(OutgoingPacket packet) {
        var listener = new TaskCompletionSource<IncomingPacket>();
        _responseListeners.Add(packet.RequestId, listener);
        Send(packet);
        return listener.Task;
    }

    public void Start(CancellationToken? cancellationToken) {
        this._connection.ConnectTo(this.Config.IP, this.Config.Port);
        this.WaitForResponse(cancellationToken);
    }

    private void WaitForResponse(CancellationToken? cancellationToken) {
        while (true)
        {
            if (cancellationToken?.IsCancellationRequested == true)
                break;

            if (this.Connection.State == ConnectionState.Connected)
                break;

            if (this.Connection.State == ConnectionState.Disconnected)
                break;

            Thread.Yield();
        }
    }
}
