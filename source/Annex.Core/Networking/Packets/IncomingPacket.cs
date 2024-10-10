using System.Diagnostics;

namespace Annex.Core.Networking.Packets
{
    public class IncomingPacket : IPacket
    {
        private readonly MemoryStream _memoryStream;
        private readonly BinaryReader _reader;

        public string OriginalRequestId { get; }

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
        public void TraceRead(dynamic value) {
            if (this._traceId != null)
            {
                Console.WriteLine(value);
            }
        }

        public IncomingPacket(byte[] data) {
            this._memoryStream = new MemoryStream(data);
            this._reader = new BinaryReader(this._memoryStream);
            this.OriginalRequestId = ReadString();
        }

        public byte[] ReadBytes() {
            int length = this._reader.ReadInt32();
            var data = this._reader.ReadBytes(length);
            this.TraceRead(length + " bytes");
            return data;
        }

        public decimal ReadDecimal() {
            var value = this._reader.ReadDecimal();
            this.TraceRead(value);
            return value;
        }

        public float ReadFloat() {
            var value = this._reader.ReadSingle();
            this.TraceRead(value);
            return value;
        }

        public double ReadDouble() {
            var value = this._reader.ReadDouble();
            this.TraceRead(value);
            return value;
        }

        public string ReadString() {
            var value = this._reader.ReadString();
            this.TraceRead(value);
            return value;
        }

        public char ReadChar() {
            var value = this._reader.ReadChar();
            this.TraceRead(value);
            return value;
        }

        public ulong ReadULong() {
            var value = this._reader.ReadUInt64();
            this.TraceRead(value);
            return value;
        }

        public long ReadLong() {
            var value = this._reader.ReadInt64();
            this.TraceRead(value);
            return value;
        }

        public uint ReadUInt() {
            var value = this._reader.ReadUInt32();
            this.TraceRead(value);
            return value;
        }

        public int ReadInt() {
            var value = this._reader.ReadInt32();
            this.TraceRead(value);
            return value;
        }

        public int ReadUShort() {
            var value = this._reader.ReadUInt16();
            this.TraceRead(value);
            return value;
        }

        public short ReadShort() {
            var value = this._reader.ReadInt16();
            this.TraceRead(value);
            return value;
        }

        public byte ReadByte() {
            var value = this._reader.ReadByte();
            this.TraceRead(value);
            return value;
        }

        public sbyte ReadSByte() {
            var value = this._reader.ReadSByte();
            this.TraceRead(value);
            return value;
        }

        public bool ReadBool() {
            var value = this._reader.ReadBoolean();
            this.TraceRead(value);
            return value;
        }

        public byte Write() {
            var value = this._reader.ReadByte();
            this.TraceRead(value);
            return value;
        }

        public void Dispose() {
            this._reader.Dispose();
            this._memoryStream.Dispose();
        }

#if DEBUG
        public byte[] ReadBytes(string id) {
            int length = this._reader.ReadInt32();
            var data = this._reader.ReadBytes(length);
            this.TraceRead(id + ": " + length + " bytes");
            return data;
        }

        public decimal ReadDecimal(string id) {
            var value = this._reader.ReadDecimal();
            this.TraceRead(id + ": " + value);
            return value;
        }

        public float ReadFloat(string id) {
            var value = this._reader.ReadSingle();
            this.TraceRead(id + ": " + value);
            return value;
        }

        public double ReadDouble(string id) {
            var value = this._reader.ReadDouble();
            this.TraceRead(id + ": " + value);
            return value;
        }

        public string ReadString(string id) {
            var value = this._reader.ReadString();
            this.TraceRead(id + ": " + value);
            return value;
        }

        public char ReadChar(string id) {
            var value = this._reader.ReadChar();
            this.TraceRead(id + ": " + value);
            return value;
        }

        public ulong ReadULong(string id) {
            var value = this._reader.ReadUInt64();
            this.TraceRead(id + ": " + value);
            return value;
        }

        public long ReadLong(string id) {
            var value = this._reader.ReadInt64();
            this.TraceRead(id + ": " + value);
            return value;
        }

        public uint ReadUInt(string id) {
            var value = this._reader.ReadUInt32();
            this.TraceRead(id + ": " + value);
            return value;
        }

        public int ReadInt(string id) {
            var value = this._reader.ReadInt32();
            this.TraceRead(id + ": " + value);
            return value;
        }

        public int ReadUShort(string id) {
            var value = this._reader.ReadUInt16();
            this.TraceRead(id + ": " + value);
            return value;
        }

        public short ReadShort(string id) {
            var value = this._reader.ReadInt16();
            this.TraceRead(id + ": " + value);
            return value;
        }

        public sbyte ReadSByte(string id) {
            var value = this._reader.ReadSByte();
            this.TraceRead(id + ": " + value);
            return value;
        }

        public byte Write(string id) {
            var value = this._reader.ReadByte();
            this.TraceRead(id + ": " + value);
            return value;
        }

        public bool ReadBool(string id) {
            var value = this._reader.ReadBoolean();
            this.TraceRead(id + ":" + value);
            return value;
        }
#endif
    }
}
