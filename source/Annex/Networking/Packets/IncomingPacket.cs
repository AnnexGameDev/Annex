using System;
using System.Diagnostics;
using System.IO;

namespace Annex.Networking.Packets
{
    public class IncomingPacket : IPacket
    {
        private byte[] _data;
        private MemoryStream _memoryStream;
        private BinaryReader _reader;

        public long Length => this._memoryStream.Length;

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
        public void WriteValue(dynamic value) {
            if (this._traceId != null) {
                Console.WriteLine(value);
            }
        }

        public IncomingPacket(byte[] data) {
            this._data = data;
            this._memoryStream = new MemoryStream(data);
            this._reader = new BinaryReader(this._memoryStream);
        }

        public double ReadDouble() {
            var value = this._reader.ReadDouble();
            this.WriteValue(value);
            return value;
        }

        public string ReadString() {
            var value = this._reader.ReadString();
            this.WriteValue(value);
            return value;
        }

        public int ReadInt32() {
            var value = this._reader.ReadInt32();
            this.WriteValue(value);
            return value;
        }

        public float ReadFloat() {
            var value = this._reader.ReadSingle();
            this.WriteValue(value);
            return value;
        }

        public bool ReadBool() {
            var value = this._reader.ReadBoolean();
            this.WriteValue(value);
            return value;
        }

        public byte ReadByte() {
            var value = this._reader.ReadByte();
            this.WriteValue(value);
            return value;
        }
        public void Dispose() {
            _reader.Dispose();
            _memoryStream.Dispose();
        }
    }
}
