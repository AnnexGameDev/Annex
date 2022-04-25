using Annex.Core.Helpers;
using Annex.Core.Networking.Packets;
using System.Net.Sockets;

namespace Annex.Core.Networking.Engines.DotNet.Endpoints
{
    public class TcpConnection : Connection
    {
        private readonly Socket _socket;
        private byte[] _incomingData;
        private byte[] _unprocessedData;

        public TcpConnection(Socket socket) {
            _socket = socket;

            this._incomingData = new byte[this._socket.ReceiveBufferSize];
            this._unprocessedData = new byte[0];
        }

        internal void ListenForIncomingPackets() {
            this._socket.BeginReceive(this._incomingData, 0, this._incomingData.Length, SocketFlags.None, OnReceiveCallback, null);
        }

        private void OnReceiveCallback(IAsyncResult ar) {
            int lengthOfIncomingData = this._socket.EndReceive(ar);

            if (lengthOfIncomingData == 0) {
                // Disconnection
            }

            this.QueueDataForProcessing(this._incomingData, 0, lengthOfIncomingData);
            while (this.ProcessNextIncomingPacketData()) ;

            this._socket.BeginReceive(this._incomingData, 0, this._incomingData.Length, SocketFlags.None, OnReceiveCallback, null);
        }

        private bool ProcessNextIncomingPacketData() {
            if (this._unprocessedData.Length < 4) {
                return false;
            }

            // outgoing data =
            // 0000 ' message size (4 bytes)
            // 0000 ' packet id (4 bytes)
            // 0000, 0000, 0000, 0000, ... ' packet data (N bytes)
            int messageSize = BitConverter.ToInt32(this._unprocessedData, 0);
            int incomingDataSize = messageSize + 4;
            int packetSize = messageSize - 4;

            if (this._unprocessedData.Length >= incomingDataSize) {
                int packetId = BitConverter.ToInt32(this._unprocessedData, 4);
                var packetData = new byte[packetSize];
                Array.Copy(this._unprocessedData, 8, packetData, 0, packetSize);
                var packet = new IncomingPacket(packetData);

                PacketHandlerHelper.HandlePacket(this, packetId, packet);

                int newUnprocessedDataSize = this._unprocessedData.Length - incomingDataSize;
                var newUnprocessedData = new byte[newUnprocessedDataSize];
                Array.Copy(this._unprocessedData, incomingDataSize, newUnprocessedData, 0, newUnprocessedDataSize);
                this._unprocessedData = newUnprocessedData;
                return true;
            }

            return false;
        }

        private void QueueDataForProcessing(byte[] data, int start, int length) {
            var newUnprocessData = new byte[this._unprocessedData.Length + length];
            Array.Copy(this._unprocessedData, 0, newUnprocessData, 0, this._unprocessedData.Length);
            Array.Copy(data, start, newUnprocessData, this._unprocessedData.Length, length);
            this._unprocessedData = newUnprocessData;
        }

        internal void SendOutgoingPacket(OutgoingPacket packet) {
            var packetData = packet.Data();
            var packetIdData = BitConverter.GetBytes(packet.PacketId);
            var messageSizeData = BitConverter.GetBytes(packetIdData.Length + packetData.Length);
            var outgoingData = new byte[messageSizeData.Length + packetIdData.Length + packetData.Length];

            Array.Copy(messageSizeData, 0, outgoingData, 0, messageSizeData.Length);
            Array.Copy(packetIdData, 0, outgoingData, messageSizeData.Length, packetIdData.Length);
            Array.Copy(packetData, 0, outgoingData, messageSizeData.Length + packetIdData.Length, packetData.Length);

            this._socket.BeginSend(outgoingData, 0, outgoingData.Length, SocketFlags.None, OnSendCallback, null);
        }

        private void OnSendCallback(IAsyncResult ar) {
            this._socket.EndSend(ar);
        }

        protected override void Dispose(bool disposing) {
            base.Dispose(disposing);

            if (disposing) {
                this._socket.Disconnect(false);
                this._socket.Dispose();
            }
        }
    }
}
