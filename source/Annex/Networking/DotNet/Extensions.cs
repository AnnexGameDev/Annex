using System.Net.Sockets;

namespace Annex.Networking.DotNet
{
    public static class Extensions
    {
        public static SocketType GetSocketType(this TransmissionType protocol) {
            switch (protocol) {
                case TransmissionType.ReliableOrdered:
                    return SocketType.Stream;
                case TransmissionType.UnreliableUnordered:
                    return SocketType.Dgram;
                default:
                    return SocketType.Unknown;
            }
        }

        public static ProtocolType GetProtocolType(this TransmissionType protocol) {
            switch (protocol) {
                case TransmissionType.ReliableOrdered:
                    return ProtocolType.Tcp;
                case TransmissionType.UnreliableUnordered:
                    return ProtocolType.Udp;
                default:
                    return ProtocolType.Unknown;
            }
        }
    }
}
