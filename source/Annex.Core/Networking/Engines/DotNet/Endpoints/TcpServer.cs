using Annex.Core.Networking.Connections;
using Annex.Core.Networking.Packets;
using Scaffold.Logging;
using System.Net;

namespace Annex.Core.Networking.Engines.DotNet.Endpoints;

internal class TcpServer : TcpEndpoint, IServerEndpoint
{
    private readonly IPacketHandlerService _packetHandlerService;

    public event EventHandler<IConnection>? OnClientConnected;
    public event EventHandler<IConnection>? OnClientDisconnected;

    public IEnumerable<IConnection> ClientConnections => this.Connections.Values.ToArray();

    public TcpServer(EndpointConfiguration config, IPacketHandlerService packetHandlerService) : base(config) {
        _packetHandlerService = packetHandlerService;
    }

    public void Send(IConnection connection, OutgoingPacket packet) {
        if (connection is not TcpConnection tcpConnection)
        {
            Log.Error($"Connection is not a {nameof(TcpConnection)}");
            return;
        }
        this.SendTo(tcpConnection, packet);
    }

    public void SendToAll(OutgoingPacket packet) {
        // TODO: Find a less memory intense way to do this. This is potentially expensive.
        foreach (var connection in this.ClientConnections)
        {
            Send(connection, packet);
        }
    }

    public void Start() {
        this.Socket.Bind(new IPEndPoint(IPAddress.Loopback, this.Config.Port));
        this.Socket.Listen(5);
        this.Socket.BeginAccept(OnAcceptCallback, null);
    }

    private void OnAcceptCallback(IAsyncResult ar) {
        if (this.Disposed)
        {
            return;
        }

        var client = this.Socket.EndAccept(ar);

        var connection = new TcpConnection(client, HandlePacket);
        this.HandleNewConnection(connection);

        this.Socket.Listen(5);
        this.Socket.BeginAccept(OnAcceptCallback, null);
    }

    private void HandlePacket(IConnection connection, int packetId, IncomingPacket packet) {
        _packetHandlerService.HandlePacket(connection, packetId, packet);
    }

    protected override void HandleDisconnectedConnection(TcpConnection connection) {
        base.HandleDisconnectedConnection(connection);
        this.OnClientDisconnected?.Invoke(this, connection);
    }

    protected override void HandleNewConnection(TcpConnection connection) {
        base.HandleNewConnection(connection);
        this.OnClientConnected?.Invoke(this, connection);
    }
}
