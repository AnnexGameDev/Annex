using Annex_Old.Core.Networking.Connections;
using Annex_Old.Core.Networking.Packets;
using Scaffold.Logging;
using System.Net;

namespace Annex_Old.Core.Networking.Engines.DotNet.Endpoints
{
    internal class TcpServer : TcpEndpoint, IServerEndpoint
    {
        public IEnumerable<Connection> ClientConnections => this.Connections;

        public TcpServer(EndpointConfiguration config) : base(config) {
        }

        public void Send(Connection connection, OutgoingPacket packet) {
            if (connection is not TcpConnection tcpConnection) {
                Log.Trace(LogSeverity.Error, $"Connection is not a {nameof(TcpConnection)}");
                return;
            }
            this.SendTo(tcpConnection, packet);
        }

        public void Start() {
            this.Socket.Bind(new IPEndPoint(IPAddress.Loopback, this.Config.Port));
            this.Socket.Listen(5);
            this.Socket.BeginAccept(OnAcceptCallback, null);
        }

        private void OnAcceptCallback(IAsyncResult ar) {
            this.Socket.Listen(5);
            this.Socket.BeginAccept(OnAcceptCallback, null);

            var client = this.Socket.EndAccept(ar);
            var connection = new TcpConnection(client);
            this.HandleNewConnection(connection);
        }
    }
}
