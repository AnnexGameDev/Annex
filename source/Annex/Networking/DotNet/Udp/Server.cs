using Annex_Old.Networking.Configuration;

namespace Annex_Old.Networking.DotNet.Udp
{
    public class Server : UdpSocket
    {
        public Server(ServerConfiguration configuration) : base(configuration) {
        }
    }
}
