using Annex_Old.Networking.Packets;
using Annex_Old.Services;
using System.Collections.Generic;

namespace Annex_Old.Networking
{
    public class PacketHandler<T> where T : Connection
    {
        private readonly Dictionary<int, IIncomingPacketHandler<T>> _handlers;

        public PacketHandler() {
            this._handlers = new Dictionary<int, IIncomingPacketHandler<T>>();
        }

        public void AddPacketHandler(int id, IIncomingPacketHandler<T> handler) {
            this._handlers[id] = handler;
        }

        public void HandlePacket(T client, IncomingPacket packet) {
            int id = packet.ReadInt32();
            
            if (!this._handlers.ContainsKey(id)) {
                ServiceProvider.LogService?.WriteLineWarning($"No packet handler exists for packet id {id}");
                return;
            }

            this._handlers[id].Handle(client, packet);
        }
    }
}
