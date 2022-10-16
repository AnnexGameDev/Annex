using Annex.Core.Networking.Connections;
using Annex.Core.Networking.Packets;

namespace Annex.Core.Networking
{
    public interface IServerEndpoint : IEndpoint
    {
        event EventHandler<IConnection>? OnClientConnected;
        event EventHandler<IConnection>? OnClientDisconnected;

        IEnumerable<IConnection> ClientConnections { get; }
        void Send(IConnection connection, OutgoingPacket packet);
        void SendToAll(OutgoingPacket packet);
        void Start();
    }
}
