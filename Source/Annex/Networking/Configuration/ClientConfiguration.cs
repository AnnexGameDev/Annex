using Lidgren.Network;

namespace Annex.Networking.Configuration
{
    public class ClientConfiguration : SocketConfiguration
    {
        public static implicit operator NetPeerConfiguration(ClientConfiguration config) {
            return new NetPeerConfiguration(config.AppIdentifier) {
                
            };
        }
    }
}
