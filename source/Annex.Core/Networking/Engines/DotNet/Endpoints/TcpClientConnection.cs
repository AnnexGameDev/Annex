using Annex.Core.Networking.Connections;
using Scaffold.Logging;
using System.Net.Sockets;

namespace Annex.Core.Networking.Engines.DotNet.Endpoints;

internal class TcpClientConnection : TcpConnection
{
    public TcpClientConnection(Socket socket, IPacketHandlerService packetHandlerService) : base(socket, packetHandlerService.HandlePacket) {
    }

    internal async Task<bool> ConnectToAsync(string ip, int port, CancellationToken cancellationToken) {
        try
        {
            State = ConnectionState.Connecting;
            await Socket.ConnectAsync(ip, port, cancellationToken);
            State = ConnectionState.Connected;
            return Socket.Connected;
        }
        catch (Exception ex)
        {
            Log.Error($"Exception was thrown while connecting to {ip}:{port}", ex);
            State = ConnectionState.Disconnected;
            return false;
        }
    }
}
