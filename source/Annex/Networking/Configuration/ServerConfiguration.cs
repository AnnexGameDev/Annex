using Lidgren.Network;

namespace Annex.Networking.Configuration
{
    public class ServerConfiguration : SocketConfiguration
    {
        public static implicit operator NetPeerConfiguration(ServerConfiguration config) {
            return new NetPeerConfiguration(config.AppIdentifier) {
                Port = config.Port
            };
        }
    }
}
