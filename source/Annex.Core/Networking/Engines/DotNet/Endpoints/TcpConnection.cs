using Annex.Core.Networking.Connections;
using Annex.Core.Networking.Packets;
using System.Net.Sockets;

namespace Annex.Core.Networking.Engines.DotNet.Endpoints;

internal class TcpConnection : Connection
{
    public delegate void ProcessPacketHandler(IConnection connection, int packetId, IncomingPacket packet);

    protected readonly Socket Socket;
    private readonly ProcessPacketHandler _processPacketHandler;
    private byte[] _incomingData;
    private byte[] _unprocessedData;

    public TcpConnection(Socket socket, ProcessPacketHandler processPacketHandler) {
        Socket = socket;
        _processPacketHandler = processPacketHandler;

        this._incomingData = new byte[this.Socket.ReceiveBufferSize];
        this._unprocessedData = Array.Empty<byte>();
    }

    internal void ListenForIncomingPackets() {
        this.Socket.BeginReceive(this._incomingData, 0, this._incomingData.Length, SocketFlags.None, OnReceiveCallback, null);
    }

    private void OnReceiveCallback(IAsyncResult ar) {
        if (this.Disposed)
        {
            return;
        }

        int lengthOfIncomingData = 0;

        try
        {
            lengthOfIncomingData = this.Socket.EndReceive(ar);
        }
        catch (Exception e)
        {
            this.Destroy("Exception thrown when calling EndReceive", e);
            return;
        }

        if (lengthOfIncomingData == 0)
        {
            this.Destroy("Incoming data is of length 0");
            return;
        }

        this.QueueDataForProcessing(this._incomingData, 0, lengthOfIncomingData);
        while (this.ProcessNextIncomingPacketData())
            ;

        this.Socket.BeginReceive(this._incomingData, 0, this._incomingData.Length, SocketFlags.None, OnReceiveCallback, null);
    }

    private bool ProcessNextIncomingPacketData() {
        if (this._unprocessedData.Length < 4)
        {
            return false;
        }

        // outgoing data =
        // 0000 ' message size (4 bytes)
        // 0000 ' packet id (4 bytes)
        // 0000, 0000, 0000, 0000, ... ' packet data (N bytes)
        int messageSize = BitConverter.ToInt32(this._unprocessedData, 0);
        int incomingDataSize = messageSize + 4;
        int packetSize = messageSize - 4;

        if (this._unprocessedData.Length >= incomingDataSize)
        {
            int packetId = BitConverter.ToInt32(this._unprocessedData, 4);
            var packetData = new byte[packetSize];
            Array.Copy(this._unprocessedData, 8, packetData, 0, packetSize);

            using (var packet = new IncomingPacket(packetData))
            {
                _processPacketHandler.Invoke(this, packetId, packet);
            }

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

    public override void Send(OutgoingPacket packet) {
        var packetData = packet.Data();
        var packetIdData = BitConverter.GetBytes(packet.PacketId);
        var messageSizeData = BitConverter.GetBytes(packetIdData.Length + packetData.Length);
        var outgoingData = new byte[messageSizeData.Length + packetIdData.Length + packetData.Length];

        Array.Copy(messageSizeData, 0, outgoingData, 0, messageSizeData.Length);
        Array.Copy(packetIdData, 0, outgoingData, messageSizeData.Length, packetIdData.Length);
        Array.Copy(packetData, 0, outgoingData, messageSizeData.Length + packetIdData.Length, packetData.Length);

        this.Socket.BeginSend(outgoingData, 0, outgoingData.Length, SocketFlags.None, OnSendCallback, null);
    }

    private void OnSendCallback(IAsyncResult ar) {
        this.Socket.EndSend(ar);
    }

    public override void Destroy(string reason, Exception? exception = null) {
        base.Destroy(reason, exception);
        this.Socket.Dispose();
    }

    public override string ToString() {
        return this.Socket?.RemoteEndPoint?.ToString() ?? string.Empty;
    }
}
