using Annex.Core.Networking.Connections;
using Annex.Core.Networking.Packets;
using Scaffold.Logging;

namespace Annex.Core.Networking;

public interface IPacketHandlerService
{
    void HandlePacket(IConnection connection, int packetId, IncomingPacket packet);
    void Init(IEnumerable<IPacketHandler> enumerable);
}

internal class PacketHandlerService : IPacketHandlerService
{
    private readonly Dictionary<int, IPacketHandler> _packetHandlers = new();

    public void HandlePacket(IConnection connection, int packetId, IncomingPacket packet) {
        if (_packetHandlers?.TryGetValue(packetId, out var handler) != true)
        {
            Log.Trace(LogSeverity.Error, $"No packet handler exists for the packet id {packetId}");
            return;
        }

        Log.Trace(LogSeverity.Verbose, $"Packet received for {connection}");
        try
        {
            handler!.Handle(connection, packet);
        }
        catch (Exception ex)
        {
            Log.Trace(LogSeverity.Error, $"Exception thrown while handling packet: {packetId} for {connection}", ex);
        }
    }

    public void Init(IEnumerable<IPacketHandler> packetHandlers) {
        if (_packetHandlers.Any())
        {
            throw new InvalidOperationException();
        }

        foreach (var handler in packetHandlers)
        {
            _packetHandlers.Add(handler.Id, handler);
        }
    }
}
