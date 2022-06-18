using Annex.Core.Networking.Connections;
using Annex.Core.Networking.Packets;

namespace Annex.Core.Networking
{
    public interface IClientEndpoint : IEndpoint
    {
        Connection Connection { get; }
        void Send(OutgoingPacket packet);
        Connection Start(long timeout = 5000);
    }
}
