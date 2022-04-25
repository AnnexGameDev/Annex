using Annex.Core.Networking.Packets;

namespace Annex.Core.Networking
{
    public interface IServerEndpoint : IEndpoint
    {
        void SendTo(Connection connection, OutgoingPacket packet);
    }
}
