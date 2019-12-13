using Annex.Networking.Configuration;

namespace Annex.Networking.DotNet.Udp
{
    public class Server : UdpSocket
    {
        public Server(ServerConfiguration configuration) : base(configuration) {
        }
    }
}
