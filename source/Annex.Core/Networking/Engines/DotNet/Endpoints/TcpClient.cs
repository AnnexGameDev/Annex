using Annex.Core.Networking.Connections;
using Annex.Core.Networking.Packets;
using Scaffold.Logging;

namespace Annex.Core.Networking.Engines.DotNet.Endpoints;

internal class TcpClient : TcpEndpoint, IClientEndpoint
{
    private TcpClientConnection _connection;
    public IConnection Connection => _connection;

    private readonly IPacketHandlerService _packetHandlerService;

    public TcpClient(EndpointConfiguration config, IPacketHandlerService packetHandlerService) : base(config) {
        this._connection = new TcpClientConnection(this.Socket, packetHandlerService);
        this._packetHandlerService = packetHandlerService;
        this.Connection.OnConnectionStateChanged += OnConnectionStateChanged;
    }

    private void OnConnectionStateChanged(object? sender, ConnectionState state) {

        Log.Normal($"Connection {this.Connection} state changed to: {state}");

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
        var responseTask = _packetHandlerService.WaitForResponseAsync(packet.RequestId);
        this.Send(packet);
        return responseTask;
    }

    public Task<bool> StartAsync(CancellationToken? cancellationToken) {
        return _connection.ConnectToAsync(Config.IP, Config.Port, cancellationToken ?? new());
    }
}
