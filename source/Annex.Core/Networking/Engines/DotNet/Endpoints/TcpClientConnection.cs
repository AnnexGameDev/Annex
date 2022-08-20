using Annex.Core.Networking.Connections;
using Scaffold.Logging;
using System.Net;
using System.Net.Sockets;

namespace Annex.Core.Networking.Engines.DotNet.Endpoints;

internal class TcpClientConnection : TcpConnection
{
    public TcpClientConnection(Socket socket) : base(socket) {
    }

    internal void ConnectTo(string iP, int port) {
        this.Socket.BeginConnect(new IPEndPoint(IPAddress.Parse(iP), port), OnConnectCallback, null);
        this.State = ConnectionState.Connecting;
    }

    private void OnConnectCallback(IAsyncResult ar) {
        try {
            this.Socket.EndConnect(ar);

            if (this.Socket.Connected) {
                this.State = ConnectionState.Connected;
            } else {
                this.State = ConnectionState.Disconnected;
            }
        }
        catch (Exception e) {
            Log.Trace(LogSeverity.Error, $"An exception was thrown while calling {nameof(OnConnectCallback)}", e);
            this.State = ConnectionState.Unknown;
        }
    }
}
