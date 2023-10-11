using Annex.Core.Networking.Connections;

namespace Annex.Core.Networking.Packets
{
    public interface IPacketHandler
    {
        int Id { get; }
        Task HandleAsync(IConnection connection, IncomingPacket packet);
    }
}
