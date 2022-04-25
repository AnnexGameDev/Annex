using Annex.Core.Helpers;
using Scaffold.DependencyInjection;

namespace Annex.Core.Networking.Engines.DotNet
{
    public class DotNetNetworkingEngine : INetworkingEngine
    {
        public DotNetNetworkingEngine(IContainer container) {
            container.Resolve<PacketHandlerHelper>();
        }

        public IClientEndpoint CreateClient(EndpointConfiguration config) {
            throw new NotImplementedException();
        }

        public IServerEndpoint CreateServer(EndpointConfiguration config) {
            throw new NotImplementedException();
        }
    }
}
