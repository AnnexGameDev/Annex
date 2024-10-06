using Annex.Core.Networking.Connections;
using Scaffold.Logging;
using System.Net;
using System.Net.Sockets;

namespace Annex.Core.Networking.Engines.DotNet.Endpoints;

internal class TcpClientConnection : TcpConnection
{
    public TcpClientConnection(Socket socket, IPacketHandlerService packetHandlerService) : base(socket, packetHandlerService.HandlePacket) {
    }

    internal void ConnectTo(string iP, int port) {
        this.State = ConnectionState.Connecting;
        this.Socket.BeginConnect(new IPEndPoint(IPAddress.Parse(iP), port), OnConnectCallback, null);
    }

    private void OnConnectCallback(IAsyncResult ar) {
        try
        {
            this.Socket.EndConnect(ar);

            if (this.Socket.Connected)
            {
                this.State = ConnectionState.Connected;
            } else
            {
                this.State = ConnectionState.Disconnected;
            }
        }
        catch (Exception e)
        {
            Log.Error($"An exception was thrown while calling {nameof(OnConnectCallback)}", e);
            this.State = ConnectionState.Unknown;
        }
    }
}
