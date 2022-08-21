using Annex.Core.Helpers;
using Annex.Core.Networking.Engines.DotNet.Endpoints;
using Scaffold.DependencyInjection;

namespace Annex.Core.Networking.Engines.DotNet
{
    public class DotNetNetworkingEngine : INetworkingEngine
    {
        public DotNetNetworkingEngine(IContainer container) {
            container.Resolve<PacketHandlerHelper>(false);
        }

        public IClientEndpoint CreateClient(EndpointConfiguration config) {
            if (config.TransmissionType == TransmissionType.ReliableOrdered) {
                return new TcpClient(config);
            }

            throw new InvalidOperationException($"No {nameof(IClientEndpoint)} could be created from {config}");
        }

        public IServerEndpoint CreateServer(EndpointConfiguration config) {
            if (config.TransmissionType == TransmissionType.ReliableOrdered) {
                return new TcpServer(config);
            }

            throw new InvalidOperationException($"No {nameof(IServerEndpoint)} could be created from {config}");
        }
    }
}
