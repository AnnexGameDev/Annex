using System.Diagnostics;

namespace Annex.Core.Networking.Packets;

public class OutgoingPacket : IPacket
{
    private readonly MemoryStream _memoryStream;
    private readonly BinaryWriter _writer;
    public readonly string RequestId;

    public long Length => _memoryStream.Length;

    public int PacketId { get; }

    private string? _traceId;

    [Conditional("DEBUG")]
    public void StartTrace(string id) {
        Console.WriteLine($"START {id}");
        _traceId = id;
    }

    [Conditional("DEBUG")]
    public void StopTrace() {
        Console.WriteLine($"END {_traceId}");
        _traceId = null;
    }

    [Conditional("DEBUG")]
    public void TraceWrite(dynamic value) {
        if (_traceId != null)
        {
            Console.WriteLine(value);
        }
    }

    private OutgoingPacket(int packetId, string requestId) {
        _memoryStream = new MemoryStream();
        _writer = new BinaryWriter(_memoryStream);
        PacketId = packetId;
        RequestId = requestId;
        Write(RequestId);
    }

    public OutgoingPacket(int packetId) : this(packetId, Guid.NewGuid().ToString()) {
    }

    public OutgoingPacket(IncomingPacket packet) : this(IPacket.ResponsePacketId, packet.OriginalRequestId) {
    }

    public byte[] Data() {
        return _memoryStream.ToArray();
    }

    public void Write(bool data) {
        _writer.Write(data);
        TraceWrite(data);
    }

    public void Write(byte[] data) {
        _writer.Write(data.Length);
        _writer.Write(data);
        TraceWrite(data.Length + " bytes");
    }

    public void Write(decimal value) {
        _writer.Write(value);
        TraceWrite(value);
    }

    public void Write(float value) {
        _writer.Write(value);
        TraceWrite(value);
    }

    public void Write(double value) {
        _writer.Write(value);
        TraceWrite(value);
    }

    public void Write(string value) {
        _writer.Write(value);
        TraceWrite(value);
    }

    public void Write(char value) {
        _writer.Write(value);
        TraceWrite(value);
    }

    public void Write(ulong value) {
        _writer.Write(value);
        TraceWrite(value);
    }

    public void Write(long value) {
        _writer.Write(value);
        TraceWrite(value);
    }

    public void Write(uint value) {
        _writer.Write(value);
        TraceWrite(value);
    }

    public void Write(int value) {
        _writer.Write(value);
        TraceWrite(value);
    }

    public void Write(ushort value) {
        _writer.Write(value);
        TraceWrite(value);
    }

    public void Write(short value) {
        _writer.Write(value);
        TraceWrite(value);
    }

    public void Write(sbyte value) {
        _writer.Write(value);
        TraceWrite(value);
    }

    public void Write(byte value) {
        _writer.Write(value);
        TraceWrite(value);
    }

    public void Dispose() {
        _writer.Dispose();
        _memoryStream.Dispose();
    }

#if DEBUG
    [Conditional("DEBUG")]
    public void Write(string id, byte[] data) {
        _writer.Write(data.Length);
        _writer.Write(data);
        TraceWrite(id + ": " + data.Length + " bytes");
    }

    [Conditional("DEBUG")]
    public void Write(string id, decimal value) {
        _writer.Write(value);
        TraceWrite(id + ": " + value);
    }

    [Conditional("DEBUG")]
    public void Write(string id, float value) {
        _writer.Write(value);
        TraceWrite(id + ": " + value);
    }

    [Conditional("DEBUG")]
    public void Write(string id, double value) {
        _writer.Write(value);
        TraceWrite(id + ": " + value);
    }

    [Conditional("DEBUG")]
    public void Write(string id, string value) {
        _writer.Write(value);
        TraceWrite(id + ": " + value);
    }

    [Conditional("DEBUG")]
    public void Write(string id, char value) {
        _writer.Write(value);
        TraceWrite(id + ": " + value);
    }

    [Conditional("DEBUG")]
    public void Write(string id, ulong value) {
        _writer.Write(value);
        TraceWrite(id + ": " + value);
    }

    [Conditional("DEBUG")]
    public void Write(string id, long value) {
        _writer.Write(value);
        TraceWrite(id + ": " + value);
    }

    [Conditional("DEBUG")]
    public void Write(string id, uint value) {
        _writer.Write(value);
        TraceWrite(id + ": " + value);
    }

    [Conditional("DEBUG")]
    public void Write(string id, int value) {
        _writer.Write(value);
        TraceWrite(id + ": " + value);
    }

    [Conditional("DEBUG")]
    public void Write(string id, ushort value) {
        _writer.Write(value);
        TraceWrite(id + ": " + value);
    }

    [Conditional("DEBUG")]
    public void Write(string id, short value) {
        _writer.Write(value);
        TraceWrite(id + ": " + value);
    }

    [Conditional("DEBUG")]
    public void Write(string id, sbyte value) {
        _writer.Write(value);
        TraceWrite(id + ": " + value);
    }

    [Conditional("DEBUG")]
    public void Write(string id, byte value) {
        _writer.Write(value);
        TraceWrite(id + ": " + value);
    }
#endif
}
