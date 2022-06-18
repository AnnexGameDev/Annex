using Annex.Core.Networking.Connections;
using System.Net;
using System.Net.Sockets;

namespace Annex.Core.Networking.Engines.DotNet.Endpoints
{
    internal class TcpClientConnection : TcpConnection
    {
        public TcpClientConnection(Socket socket) : base(socket) {
        }

        internal void ConnectTo(string iP, int port) {
            this.Socket.BeginConnect(new IPEndPoint(IPAddress.Parse(iP), port), OnConnectCallback, null);
            this.State = ConnectionState.Connecting;
        }

        private void OnConnectCallback(IAsyncResult ar) {
            this.Socket.EndConnect(ar);

            if (this.Socket.Connected) {
                this.State = ConnectionState.Connected;
            } else {
                this.State = ConnectionState.Disconnected;
            }
        }
    }
}
