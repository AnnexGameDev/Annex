using System.Diagnostics;

namespace Annex.Core.Networking.Packets
{
    public class OutgoingPacket : IPacket
    {
        private readonly MemoryStream _memoryStream;
        private readonly BinaryWriter _writer;

        public long Length => this._memoryStream.Length;

        public int PacketId { get; }

        private string? _traceId;

        [Conditional("DEBUG")]
        public void StartTrace(string id) {
            Console.WriteLine($"START {id}");
            this._traceId = id;
        }

        [Conditional("DEBUG")]
        public void StopTrace() {
            Console.WriteLine($"END {this._traceId}");
            this._traceId = null;
        }

        [Conditional("DEBUG")]
        public void TraceWrite(dynamic value) {
            if (this._traceId != null) {
                Console.WriteLine(value);
            }
        }

        private OutgoingPacket() {
            this._memoryStream = new MemoryStream();
            this._writer = new BinaryWriter(this._memoryStream);
        }

        public OutgoingPacket(int packetId) : this() {
            this.PacketId = packetId;
        }

        public byte[] Data() {
            return this._memoryStream.ToArray();
        }

        public void Write(bool data) {
            this._writer.Write(data);
            this.TraceWrite(data);
        }

        public void Write(byte[] data) {
            this._writer.Write(data.Length);
            this._writer.Write(data);
            this.TraceWrite(data.Length + " bytes");
        }

        public void Write(decimal value) {
            this._writer.Write(value);
            this.TraceWrite(value);
        }

        public void Write(float value) {
            this._writer.Write(value);
            this.TraceWrite(value);
        }

        public void Write(double value) {
            this._writer.Write(value);
            this.TraceWrite(value);
        }

        public void Write(string value) {
            this._writer.Write(value);
            this.TraceWrite(value);
        }

        public void Write(char value) {
            this._writer.Write(value);
            this.TraceWrite(value);
        }

        public void Write(ulong value) {
            this._writer.Write(value);
            this.TraceWrite(value);
        }

        public void Write(long value) {
            this._writer.Write(value);
            this.TraceWrite(value);
        }

        public void Write(uint value) {
            this._writer.Write(value);
            this.TraceWrite(value);
        }

        public void Write(int value) {
            this._writer.Write(value);
            this.TraceWrite(value);
        }

        public void Write(ushort value) {
            this._writer.Write(value);
            this.TraceWrite(value);
        }

        public void Write(short value) {
            this._writer.Write(value);
            this.TraceWrite(value);
        }

        public void Write(sbyte value) {
            this._writer.Write(value);
            this.TraceWrite(value);
        }

        public void Write(byte value) {
            this._writer.Write(value);
            this.TraceWrite(value);
        }

        public void Dispose() {
            this._writer.Dispose();
            this._memoryStream.Dispose();
        }

#if DEBUG
        [Conditional("DEBUG")]
        public void Write(string id, byte[] data) {
            this._writer.Write(data.Length);
            this._writer.Write(data);
            this.TraceWrite(id + ": " + data.Length + " bytes");
        }

        [Conditional("DEBUG")]
        public void Write(string id, decimal value) {
            this._writer.Write(value);
            this.TraceWrite(id + ": " + value);
        }

        [Conditional("DEBUG")]
        public void Write(string id, float value) {
            this._writer.Write(value);
            this.TraceWrite(id + ": " + value);
        }

        [Conditional("DEBUG")]
        public void Write(string id, double value) {
            this._writer.Write(value); 
            this.TraceWrite(id + ": " + value);
        }

        [Conditional("DEBUG")]
        public void Write(string id, string value) {
            this._writer.Write(value);
            this.TraceWrite(id + ": " + value);
        }

        [Conditional("DEBUG")]
        public void Write(string id, char value) {
            this._writer.Write(value);
            this.TraceWrite(id + ": " + value);
        }

        [Conditional("DEBUG")]
        public void Write(string id, ulong value) {
            this._writer.Write(value);
            this.TraceWrite(id + ": " + value);
        }

        [Conditional("DEBUG")]
        public void Write(string id, long value) {
            this._writer.Write(value);
            this.TraceWrite(id + ": " + value);
        }

        [Conditional("DEBUG")]
        public void Write(string id, uint value) {
            this._writer.Write(value);
            this.TraceWrite(id + ": " + value);
        }

        [Conditional("DEBUG")]
        public void Write(string id, int value) {
            this._writer.Write(value);
            this.TraceWrite(id + ": " + value);
        }

        [Conditional("DEBUG")]
        public void Write(string id, ushort value) {
            this._writer.Write(value);
            this.TraceWrite(id + ": " + value);
        }

        [Conditional("DEBUG")]
        public void Write(string id, short value) {
            this._writer.Write(value);
            this.TraceWrite(id + ": " + value);
        }

        [Conditional("DEBUG")]
        public void Write(string id, sbyte value) {
            this._writer.Write(value);
            this.TraceWrite(id + ": " + value);
        }

        [Conditional("DEBUG")]
        public void Write(string id, byte value) {
            this._writer.Write(value);
            this.TraceWrite(id + ": " + value);
        }
#endif
    }
}
