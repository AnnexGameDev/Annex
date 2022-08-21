using Annex.Core.Networking.Connections;
using Annex.Core.Networking.Packets;
using Scaffold.DependencyInjection;
using Scaffold.Logging;
using System.Reflection;

namespace Annex.Core.Helpers
{
    internal class PacketHandlerHelper
    {
        private static Dictionary<int, Type> _handlers;
        private static IContainer _container;

        static PacketHandlerHelper() {
            var asm = Assembly.GetEntryAssembly();
            var packetHandlers = asm.GetTypes().Where(type => type.GetCustomAttribute<PacketHandlerAttribute>() != null);

            _handlers = new();
            foreach (var handlerType in packetHandlers) {
                int packetId = handlerType.GetCustomAttribute<PacketHandlerAttribute>()!.PacketId;
                Log.Trace(LogSeverity.Verbose, $"Registering packet handler: {packetId} -> {handlerType.Name}");
                _handlers.Add(packetId, handlerType);
            }
        }

        public PacketHandlerHelper(IContainer container) {
            if (_container != null) {
                Log.Trace(LogSeverity.Warning, $"{nameof(PacketHandlerHelper)} is being instanciated multiple times");
                return;
            }

            _container = container;
        }

        public static void HandlePacket(IConnection connection, int packetId, IncomingPacket packet) {


            if (_handlers?.TryGetValue(packetId, out var handlerType) != true) {
                Log.Trace(LogSeverity.Error, $"No packet handler exists for the packet id {packetId}");
                return;
            }

            var handler = _container.Resolve(handlerType) as IPacketHandler;

            if (handler == null) {
                Log.Trace(LogSeverity.Error, $"The type {handlerType.Name} can't be casted to {nameof(IPacketHandler)}");
                return;
            }
            handler!.Handle(connection, packet);
        }
    }
}
