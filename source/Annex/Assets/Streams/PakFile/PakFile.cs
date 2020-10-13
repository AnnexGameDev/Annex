using System;
using System.Collections.Generic;
using System.IO;

namespace Annex.Assets.Streams.PakFile
{
    public class PakFile : IDisposable
    {
        private readonly Dictionary<string, PakFileEntry> _entries;

        private readonly FileStream _fs;
        private readonly BufferedStream _bs;
        private readonly BinaryReader _br;

        public PakFile(string filepath) {
            this._entries = new Dictionary<string, PakFileEntry>();
            this.AddEntriesFrom(filepath);

            this._fs = new FileStream(filepath, FileMode.Open);
            this._bs = new BufferedStream(this._fs);
            this._br = new BinaryReader(this._bs);
        }

        public void Dispose() {
            this._br.Close();
            this._bs.Close();
            this._fs.Close();
        }

        public byte[] GetEntry(string id) {
            Debug.Assert(this._entries.ContainsKey(id), $"PakFile does not contain the entry {id}");
            var entry = this._entries[id];

            this._br.BaseStream.Seek(entry.Position, SeekOrigin.Begin);
            return this._br.ReadBytes(entry.Size);
        }

        private void AddEntriesFrom(string filepath) {
            Debug.Assert(File.Exists(filepath), $"The PakFile {filepath} does not exist");
            using var ms = new MemoryStream(File.ReadAllBytes(filepath));
            using var br = new BinaryReader(ms);

            int count = br.ReadInt32();
            for (int i = 0; i < count; i++) {
                string id = br.ReadString();
                int length = br.ReadInt32();
                long position = br.BaseStream.Position;
                br.ReadBytes(length);

                this._entries.Add(id, new PakFileEntry(position, length));
            }
        }
    }
}
