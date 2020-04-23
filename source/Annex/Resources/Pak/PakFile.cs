using System;
using System.Collections.Generic;
using System.IO;

namespace Annex.Resources.Pak
{
    public class PakFile : IDisposable
    {
        private readonly Dictionary<string, PakFileEntry> _entries;
        private long _offset;
        private readonly FileStream _fs;
        private readonly BufferedStream _bs;
        private readonly BinaryReader _br;
        private readonly bool _canWrite;

        public static PakFile CreateForInput(string path) {
            return new PakFile(path);
        }

        public static PakFile CreateForOutput() {
            return new PakFile();
        }

        private PakFile() {
            this._entries = new Dictionary<string, PakFileEntry>();
            this._canWrite = true;
        }

        private PakFile(string path) {
            Debug.Assert(File.Exists(path));
            this._canWrite = false;
            this._entries = new Dictionary<string, PakFileEntry>();
            this._fs = new FileStream(path, FileMode.Open);
            this._bs = new BufferedStream(this._fs);
            this._br = new BinaryReader(this._bs);

            int numEntries = this._br.ReadInt32();
            for (int i = 0; i < numEntries; i++) {
                string id = this._br.ReadString();
                long position = this._br.ReadInt64();
                int size = this._br.ReadInt32();
                this._entries[id] = new PakFileEntry(position, size);
            }
            this._offset = this._bs.Position;
        }

        public byte[] GetEntry(string id) {
            Debug.Assert(this._entries.ContainsKey(id));
            if (this._canWrite) {
                // TODO: Throw?
                return null;
            }
            var entry = this._entries[id];
            this._br.BaseStream.Seek(this._offset + entry.Position, SeekOrigin.Begin);
            return this._br.ReadBytes(entry.Size);
        }

        public void AddEntry(string id, byte[] data) {
            if (!this._canWrite) {
                // TODO: Throw? Assert?
                return;
            }
            this._entries.Add(id, new PakFileEntry(this._offset, data.Length, data));
            this._offset += data.Length;
        }

        public void Save(string path) {
            using var ms = new MemoryStream();
            using var br = new BinaryWriter(ms);

            br.Write(this._entries.Count);
            foreach (var entry in this._entries) {
                br.Write(entry.Key);
                br.Write(entry.Value.Position);
                br.Write(entry.Value.Size);
            }
            foreach (var entry in this._entries) {
                br.Write(entry.Value.Data);
            }
            File.WriteAllBytes(path, ms.ToArray());
        }

        public void Dispose() {
            _br.Dispose();
            _bs.Dispose();
            _fs.Dispose();
        }
    }
}
