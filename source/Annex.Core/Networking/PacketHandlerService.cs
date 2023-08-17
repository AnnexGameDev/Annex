using Annex.Core.Networking.Connections;
using Annex.Core.Networking.Packets;
using Scaffold.Logging;

namespace Annex.Core.Networking;

public interface IPacketHandlerService
{
    void HandlePacket(IConnection connection, int packetId, IncomingPacket packet);
}

internal class PacketHandlerService : IPacketHandlerService
{
    private readonly Dictionary<int, IPacketHandler> _packetHandlers = new();

    public PacketHandlerService(IEnumerable<IPacketHandler> packetHandlers) {
        foreach (IPacketHandler handler in packetHandlers)
        {
            _packetHandlers.Add(handler.Id, handler);
        }
    }

    public void HandlePacket(IConnection connection, int packetId, IncomingPacket packet) {
        if (_packetHandlers?.TryGetValue(packetId, out var handler) != true)
        {
            Log.Trace(LogSeverity.Error, $"No packet handler exists for the packet id {packetId}");
            return;
        }

        Log.Trace(LogSeverity.Verbose, $"Packet received for {connection}");
        handler!.Handle(connection, packet);
    }
}
