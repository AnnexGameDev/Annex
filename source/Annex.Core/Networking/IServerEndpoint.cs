using Annex.Core.Networking.Packets;

namespace Annex.Core.Networking
{
    public interface IServerEndpoint : IEndpoint
    {
        IEnumerable<Connection> ClientConnections { get; }
        void Send(Connection connection, OutgoingPacket packet);
    }
}
