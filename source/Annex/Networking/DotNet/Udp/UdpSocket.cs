using Annex.Networking.Configuration;
using Annex.Networking.Packets;
using System;
using System.Net;
using System.Net.Sockets;

namespace Annex.Networking.DotNet.Udp
{
    public class UdpSocket : CoreSocket
    {
        private UdpClient _socket;
        private const int ANY_PORT = 0;

        public UdpSocket(ServerConfiguration config) : base(config) {
            this._socket = new UdpClient(config.Port, AddressFamily.InterNetwork);
        }

        public UdpSocket(ClientConfiguration config) : base(config) {
            this._socket = new UdpClient(ANY_PORT, AddressFamily.InterNetwork);
        }

        public override void Start() {
            this._socket.BeginReceive(this.ReceiveCallback, null);
        }

        private void ReceiveCallback(IAsyncResult ar) {
            this._socket.BeginReceive(this.ReceiveCallback, null);

            var clientEndpoint = new IPEndPoint(IPAddress.Any, 0);
            byte[] data = this._socket.EndReceive(ar, ref clientEndpoint);
            this.ReceivePacket(clientEndpoint, data);
        }

        public override void Destroy() {
            this._socket.Close();
        }

        public override void SendPacket(object baseConnection, int packetID, OutgoingPacket packet) {
            this.SendPacket((IPEndPoint)baseConnection, packetID, packet);
        }

        private void SendPacket(IPEndPoint endpoint, int packetID, OutgoingPacket packet) {
            byte[] payload = packet.GetBytes();
            byte[] datagram = new byte[payload.Length + 4];
            Array.Copy(payload, 0, datagram, 4, payload.Length);
            Array.Copy(BitConverter.GetBytes(packetID), 0, datagram, 0, 4);
            this._socket.BeginSend(datagram, datagram.Length, endpoint, this.SendCallback, null);
        }

        private void SendCallback(IAsyncResult ar) {
            this._socket.EndSend(ar);
        }
    }
}
