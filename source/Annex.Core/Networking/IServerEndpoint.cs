using Annex.Core.Networking.Connections;
using Annex.Core.Networking.Packets;

namespace Annex.Core.Networking
{
    public interface IServerEndpoint : IEndpoint
    {
        IEnumerable<IConnection> ClientConnections { get; }
        void Send(IConnection connection, OutgoingPacket packet);
        void Start();
    }
}
