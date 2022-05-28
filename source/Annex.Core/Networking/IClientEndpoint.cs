using Annex_Old.Core.Networking.Connections;
using Annex_Old.Core.Networking.Packets;

namespace Annex_Old.Core.Networking
{
    public interface IClientEndpoint : IEndpoint
    {
        Connection Connection { get; }
        void Send(OutgoingPacket packet);
        Connection Start(long timeout = 5000);
    }
}
