using Annex.Networking.Packets;
using System;
using System.Collections.Generic;

namespace Annex.Networking
{
    public class PacketHandler<T> where T : Connection
    {
        private readonly Dictionary<int, Action<T, IncomingPacket>> _handlers;

        public PacketHandler() {
            this._handlers = new Dictionary<int, Action<T, IncomingPacket>>();
        }

        public void AddPacketHandler(int id, Action<T, IncomingPacket> handler) {
            this._handlers[id] = handler;
        }

        public void HandlePacket(T client, IncomingPacket packet) {
            int id = packet.ReadInt32();
            this._handlers[id].Invoke(client, packet);
        }
    }
}
