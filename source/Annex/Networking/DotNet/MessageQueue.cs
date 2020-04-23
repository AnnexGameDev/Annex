using Annex.Events;
using Annex.Networking.Packets;
using System.Collections.Generic;

namespace Annex.Networking.DotNet
{
    public class MessageQueue<T> where T : Connection, new()
    {
        private Queue<(int id, byte[] data)> _messagesToProcess;
        private SocketEndpoint<T> _endpoint;

        public MessageQueue(SocketEndpoint<T> endpoint) {
            this._messagesToProcess = new Queue<(int id, byte[] data)>();
            this._endpoint = endpoint;
        }

        public void OnReceive(object baseConnection, byte[] data) {
            var connection = this._endpoint.Connections.CreateIfNotExistsAndGet(baseConnection, this._endpoint);

            lock (this._messagesToProcess) {
                this._messagesToProcess.Enqueue(((int)connection.ID, data));
            }
        }

        public ControlEvent ProcessQueue() {
            lock (this._messagesToProcess) {
                while (this._messagesToProcess.Count != 0) {
                    (int id, byte[] data) = this._messagesToProcess.Dequeue();
                    var connection = this._endpoint.Connections.Get(id);
                    using var packet = new IncomingPacket(data);
                    this._endpoint.PacketHandler.HandlePacket(connection, packet);
                }
            }
            return ControlEvent.NONE;
        }
    }
}
