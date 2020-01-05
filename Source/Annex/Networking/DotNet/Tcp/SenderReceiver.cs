#nullable enable
using Annex.Networking.Packets;
using System;
using System.Net.Sockets;

namespace Annex.Networking.DotNet.Tcp
{
    public class SenderReceiver
    {
        private readonly Socket _socket;
        private readonly CoreSocket _endpoint;
        private byte[] _receiveBuffer;
        private byte[] _processingBuffer;

        public SenderReceiver(Socket baseSocket, CoreSocket endpoint) {
            this._socket = baseSocket;
            this._endpoint = endpoint;

            this._receiveBuffer = new byte[this._socket.ReceiveBufferSize];
            this._processingBuffer = new byte[0];

            this._socket.BeginReceive(this._receiveBuffer, 0, this._receiveBuffer.Length, SocketFlags.None, ReceiveCallback, null);
        }

        private void ReceiveCallback(IAsyncResult ar) {

            int lengthOfIncomingData = 0;

            try {
                lengthOfIncomingData = this._socket.EndReceive(ar);
            } catch (SocketException e) {
                int x = 0;
            }

            if (lengthOfIncomingData == 0) {
                Destroy();
                return;
            }

            lock (this._processingBuffer) {
                this.MergeBuffers(lengthOfIncomingData);
                this.ProcessBuffer();
            }

            this._socket.BeginReceive(this._receiveBuffer, 0, this._receiveBuffer.Length, SocketFlags.None, ReceiveCallback, null);
        }

        private void ProcessBuffer() {
            if (this._processingBuffer.Length < 4) {
                return;
            }

            int packetPayloadSize = BitConverter.ToInt32(this._processingBuffer, 0);
            int totalPacketSize = packetPayloadSize + 4;

            if (this._processingBuffer.Length >= totalPacketSize) {
                var packet = new byte[packetPayloadSize];
                Array.Copy(this._processingBuffer, 4, packet, 0, packetPayloadSize);
                this._endpoint.ReceivePacket(this, packet);

                int newProcessingBufferSize = this._receiveBuffer.Length - totalPacketSize;
                var newProcessingBuffer = new byte[newProcessingBufferSize];
                Array.Copy(this._receiveBuffer, totalPacketSize, newProcessingBuffer, 0, newProcessingBufferSize);
                this._processingBuffer = newProcessingBuffer;
            }
        }

        private void MergeBuffers(int lengthOfIncomingData) {
            var newProcessingBuffer = new byte[this._receiveBuffer.Length + lengthOfIncomingData];
            Array.Copy(this._receiveBuffer, 0, newProcessingBuffer, 0, this._receiveBuffer.Length);
            Array.Copy(this._receiveBuffer, 0, newProcessingBuffer, this._processingBuffer.Length, lengthOfIncomingData);
            this._receiveBuffer = new byte[this._socket.ReceiveBufferSize];
            this._processingBuffer = newProcessingBuffer;
        }

        public void SendPacket(int packetIDNumber, OutgoingPacket outgoingPacket) {
            var packetPayload = outgoingPacket.GetBytes();
            var packetID = BitConverter.GetBytes(packetIDNumber);
            var packetSize = BitConverter.GetBytes(packetPayload.Length + packetID.Length);
            var fullPacket = new byte[packetSize.Length + packetID.Length + packetPayload.Length];

            Array.Copy(packetSize, 0, fullPacket, 0, packetSize.Length);
            Array.Copy(packetID, 0, fullPacket, packetSize.Length, packetID.Length);
            Array.Copy(packetPayload, 0, fullPacket, packetID.Length + packetSize.Length, packetPayload.Length);

            this._socket.BeginSend(fullPacket, 0, fullPacket.Length, SocketFlags.None, SendCallback, null);
        }

        private void SendCallback(IAsyncResult ar) {
            this._socket.EndSend(ar);
        }

        private void Destroy() {
            this._socket.Disconnect(false);
        }
    }
}
