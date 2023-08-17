using Annex.Core.Networking.Engines.DotNet.Endpoints;

namespace Annex.Core.Networking.Engines.DotNet
{
    public class DotNetNetworkingEngine : INetworkingEngine
    {
        private readonly IPacketHandlerService _packetHandlerService;

        public DotNetNetworkingEngine(IPacketHandlerService packetHandlerService) {
            _packetHandlerService = packetHandlerService;
        }

        public IClientEndpoint CreateClient(EndpointConfiguration config) {
            if (config.TransmissionType == TransmissionType.ReliableOrdered)
            {
                return new TcpClient(config, _packetHandlerService);
            }

            throw new InvalidOperationException($"No {nameof(IClientEndpoint)} could be created from {config}");
        }

        public IServerEndpoint CreateServer(EndpointConfiguration config) {
            if (config.TransmissionType == TransmissionType.ReliableOrdered)
            {
                return new TcpServer(config, _packetHandlerService);
            }

            throw new InvalidOperationException($"No {nameof(IServerEndpoint)} could be created from {config}");
        }
    }
}
