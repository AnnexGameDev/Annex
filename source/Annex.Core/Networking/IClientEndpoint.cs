using Annex.Core.Networking.Connections;
using Annex.Core.Networking.Packets;

namespace Annex.Core.Networking
{
    public interface IClientEndpoint : IEndpoint
    {
        IConnection Connection { get; }
        void Send(OutgoingPacket packet);
        IConnection Start(long timeout = 5000, CancellationToken? cancellationToken = null);
    }
}
