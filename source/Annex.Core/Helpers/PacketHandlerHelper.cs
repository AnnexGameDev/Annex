using Annex.Core.Networking;
using Annex.Core.Networking.Packets;
using Scaffold.DependencyInjection;
using Scaffold.Logging;
using System.Reflection;

namespace Annex.Core.Helpers
{
    internal class PacketHandlerHelper
    {
        private static Dictionary<int, IPacketHandler>? _handlers;

        public PacketHandlerHelper(IContainer container) {
            if (_handlers != null) {
                throw new InvalidOperationException("Static helper resource is already instanciated");
            }

            var asm = Assembly.GetEntryAssembly();
            var packetHandlers = asm.GetTypes().Where(type => type.GetCustomAttribute<PacketHandlerAttribute>() != null);

            _handlers = new();
            foreach (var handlerType in packetHandlers) {
                int packetId = handlerType.GetCustomAttribute<PacketHandlerAttribute>()!.PacketId;
                var handler = container.Resolve(handlerType) as IPacketHandler;

                if (handler == null) {
                    Log.Trace(LogSeverity.Error, $"The type {handlerType.Name} can't be casted to {nameof(IPacketHandler)}");
                    continue;
                }
                Log.Trace(LogSeverity.Verbose, $"Registering packet handler: {packetId} -> {handlerType.Name}");
                _handlers.Add(packetId, handler);
            }
        }

        public static void HandlePacket(Connection connection, int packetId, IncomingPacket packet) {

            if (_handlers?.TryGetValue(packetId, out var handler) != true) {
                Log.Trace(LogSeverity.Error, $"No packet handler exists for the packet id {packetId}");
                return;
            }

            handler!.Handle(connection, packet);
        }
    }
}
