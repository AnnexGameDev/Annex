using System;
using System.Diagnostics;
using System.IO;

namespace Annex_Old.Networking.Packets
{
    public class OutgoingPacket : IPacket
    {
        private readonly MemoryStream _memoryStream;
        private readonly BinaryWriter _writer;

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

        public long Length => this._memoryStream.Length;

        public OutgoingPacket() {
            this._memoryStream = new MemoryStream();
            this._writer = new BinaryWriter(this._memoryStream);
        }

        public byte[] GetBytes() {
            return this._memoryStream.ToArray();
        }

        public void Write(float value) {
            this._writer.Write(value);
            this.WriteValue(value);
        }

        public void Write(double value) {
            this._writer.Write(value);
            this.WriteValue(value);
        }

        public void Write(string value) {
            this._writer.Write(value);
            this.WriteValue(value);
        }

        public void Write(int value) {
            this._writer.Write(value);
            this.WriteValue(value);
        }

        public void Write(bool value) {
            this._writer.Write(value);
            this.WriteValue(value);
        }

        public void Write(byte value) {
            this._writer.Write(value);
            this.WriteValue(value);
        }

        public void Dispose() {
            this._writer.Dispose();
            this._memoryStream.Dispose();
        }
    }
}
