using Annex.Networking.Configuration;
using Annex.Networking.Packets;
using System;
using System.Net;
using System.Net.Sockets;

namespace Annex.Networking.DotNet.Tcp
{
    public class Server : CoreSocket
    {
        private Socket _listener;

        public Server(ServerConfiguration config) : base(config) {
        }

        public override void Start() {
            this._listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this._listener.Bind(new IPEndPoint(IPAddress.Loopback, this._config.Port));
            this._listener.Listen(5);
            this._listener.BeginAccept(AcceptCallback, null);
        }

        private void AcceptCallback(IAsyncResult ar) {
            this._listener.Listen(5);
            this._listener.BeginAccept(AcceptCallback, null);

            var clientSocket = this._listener.EndAccept(ar);
            new SenderReceiver(clientSocket, this);
        }

        public override void Destroy() {
            this._listener.Disconnect(false);
        }

        public override void SendPacket(object baseConnection, int packetID, OutgoingPacket packet) {
            var tcpClientConnection = (SenderReceiver)baseConnection;
            tcpClientConnection.SendPacket(packetID, packet);
        }
    }
}
