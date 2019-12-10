using System.Net.Sockets;

namespace Annex.Networking.Core
{
    public static class Extensions
    {
        public static SocketType GetSocketType(this Protocol protocol) {
            switch (protocol) {
                case Protocol.TCP:
                    return SocketType.Stream;
                case Protocol.UDP:
                    return SocketType.Dgram;
                default:
                    return SocketType.Unknown;
            }
        }

        public static ProtocolType GetProtocolType(this Protocol protocol) {
            switch (protocol) {
                case Protocol.TCP:
                    return ProtocolType.Tcp;
                case Protocol.UDP:
                    return ProtocolType.Udp;
                default:
                    return ProtocolType.Unknown;
            }
        }
    }
}
