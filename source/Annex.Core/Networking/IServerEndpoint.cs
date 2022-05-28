using Annex_Old.Core.Networking.Connections;
using Annex_Old.Core.Networking.Packets;

namespace Annex_Old.Core.Networking
{
    public interface IServerEndpoint : IEndpoint
    {
        IEnumerable<Connection> ClientConnections { get; }
        void Send(Connection connection, OutgoingPacket packet);
        void Start();
    }
}
