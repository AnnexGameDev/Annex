using Annex.Core.Networking.Connections;
using Annex.Core.Networking.Packets;
using Scaffold.Logging;
using System.Collections.Concurrent;

namespace Annex.Core.Networking;

public interface IPacketHandlerService
{
    void HandlePacket(IConnection connection, int packetId, IncomingPacket packet);
    void Init(IEnumerable<IPacketHandler> enumerable);
    Task<IncomingPacket> WaitForResponseAsync(string requestId);
}

internal class PacketHandlerService : IPacketHandlerService
{
    private readonly IDictionary<string, TaskCompletionSource<IncomingPacket>> _responseListeners;
    private readonly Dictionary<int, IPacketHandler> _packetHandlers;

    public PacketHandlerService() {
        _responseListeners = new ConcurrentDictionary<string, TaskCompletionSource<IncomingPacket>>();
        _packetHandlers = new Dictionary<int, IPacketHandler>();
    }

    public async void HandlePacket(IConnection connection, int packetId, IncomingPacket packet) {

        if (packetId == IPacket.ResponsePacketId)
        {
            Log.Trace(LogSeverity.Verbose, $"Response packet received: {packet.OriginalRequestId}");
            OnResponseReceived(packet);
            return;
        }

        if (_packetHandlers?.TryGetValue(packetId, out var handler) != true)
        {
            Log.Trace(LogSeverity.Error, $"No packet handler exists for the packet id {packetId}");
            return;
        }

        Log.Trace(LogSeverity.Verbose, $"Packet received for {connection}");
        try
        {
            await handler!.HandleAsync(connection, packet);
        }
        catch (Exception ex)
        {
            Log.Trace(LogSeverity.Error, $"Exception thrown while handling packet: {packetId} for {connection}", ex);
        }
        finally
        {
            packet.Dispose();
        }
    }

    private void OnResponseReceived(IncomingPacket packet) {

        if (_responseListeners.TryGetValue(packet.OriginalRequestId, out var listener))
        {
            listener.TrySetResult(packet);
            _responseListeners.Remove(packet.OriginalRequestId);
            return;
        }

        Log.Trace(LogSeverity.Error, $"No response handler was registered for {packet.OriginalRequestId}");
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

    public Task<IncomingPacket> WaitForResponseAsync(string requestId) {
        var listener = new TaskCompletionSource<IncomingPacket>();
        if (!_responseListeners.TryAdd(requestId, listener))
        {
            listener.SetCanceled();
        }
        return listener.Task;
    }
}
