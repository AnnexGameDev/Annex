using Annex.Networking.Configuration;
using Annex.Networking.Packets;
using System.Net;

namespace Annex.Networking.DotNet.Udp
{
    public class Client : UdpSocket
    {
        private IPEndPoint _sendingEndpoint;

        public Client(ClientConfiguration config) : base(config) {
            this._sendingEndpoint = new IPEndPoint(IPAddress.Parse(config.IP), config.Port);
        }

        public override void SendPacket(object baseConnection, int packetID, OutgoingPacket packet) {
            base.SendPacket(_sendingEndpoint, packetID, packet);
        }
    }
}
