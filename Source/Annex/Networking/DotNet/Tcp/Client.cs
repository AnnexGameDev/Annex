using Annex.Networking.Configuration;
using Annex.Networking.Packets;
using System;
using System.Net;
using System.Net.Sockets;

namespace Annex.Networking.DotNet.Tcp
{
    public class Client : CoreSocket
    {
        private Socket _socket;
        private SenderReceiver _senderReceiver;

        public Client(SocketConfiguration config) : base(config) {
        }

        public override void Start() {
            this._socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this._socket.BeginConnect(new IPEndPoint(IPAddress.Parse(this._config.IP), this._config.Port), ConnectCallback, null);
        }

        private void ConnectCallback(IAsyncResult ar) {
            this._socket.EndConnect(ar);

            this._senderReceiver = new SenderReceiver(this._socket, this);
        }

        public override void Destroy() {
            this._socket.Disconnect(false);
        }

        public override void SendPacket(object baseConnection, int packetID, OutgoingPacket packet) {
            while (this._senderReceiver == null) ;
            this._senderReceiver.SendPacket(packetID, packet);
        }
    }
}
