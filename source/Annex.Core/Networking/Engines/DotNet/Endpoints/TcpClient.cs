using Annex.Core.Networking.Packets;
using Scaffold.Logging;
using System.Net;

namespace Annex.Core.Networking.Engines.DotNet.Endpoints
{
    internal class TcpClient : TcpEndpoint, IClientEndpoint
    {
        public Connection? Connection => this.Connections.FirstOrDefault();

        public TcpClient(EndpointConfiguration config) : base(config) {
        }

        public void Send(OutgoingPacket packet) {
            if (this.Connection is not TcpConnection tcpConnection) {
                Log.Trace(LogSeverity.Error, $"Connection is not a {nameof(TcpConnection)}");
                return;
            }
            this.SendTo(tcpConnection, packet);
        }

        private void OnConnectCallback(IAsyncResult ar) {
            this.Socket.EndConnect(ar);
            this.HandleNewConnection(this.Socket);
        }

        public override void Start() {
            this.Socket.BeginConnect(new IPEndPoint(IPAddress.Parse(this.Config.IP), this.Config.Port), OnConnectCallback, null);
        }
    }
}
