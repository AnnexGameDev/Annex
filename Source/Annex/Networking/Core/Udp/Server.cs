using Annex.Networking.Configuration;

namespace Annex.Networking.Core.Udp
{
    public class Server : UdpSocket
    {
        public Server(ServerConfiguration configuration) : base(configuration) {
        }
    }
}
