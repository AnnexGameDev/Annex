namespace Annex_Old.Core.Networking
{
    public interface INetworkingEngine
    {
        IClientEndpoint CreateClient(EndpointConfiguration config);
        IServerEndpoint CreateServer(EndpointConfiguration config);
    }
}
