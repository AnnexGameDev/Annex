using Annex.Core.Networking.Packets;

namespace Annex.Core.Networking
{
    public interface IClientEndpoint : IEndpoint
    {
        void Send(OutgoingPacket packet);
    }
}
