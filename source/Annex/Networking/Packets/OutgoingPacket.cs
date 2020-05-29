using System.IO;

namespace Annex.Networking.Packets
{
    public class OutgoingPacket : IPacket
    {
        private readonly MemoryStream _memoryStream;
        private readonly BinaryWriter _writer;

        public OutgoingPacket() {
            this._memoryStream = new MemoryStream();
            this._writer = new BinaryWriter(this._memoryStream);
        }

        public byte[] GetBytes() {
            return this._memoryStream.ToArray();
        }

        public void Write(string value) {
            this._writer.Write(value);
        }

        public void Write(int value) {
            this._writer.Write(value);
        }

        public void Dispose() {
            this._writer.Dispose();
            this._memoryStream.Dispose();
        }
    }
}
