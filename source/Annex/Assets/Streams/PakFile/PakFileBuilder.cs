using System;
using System.Collections.Generic;
using System.IO;

namespace Annex.Assets.Streams.PakFile
{
    public class PakFileBuilder
    {
        private readonly Dictionary<string, byte[]> _entries;

        public PakFileBuilder() {
            this._entries = new Dictionary<string, byte[]>();
        }

        public void Add(string id, byte[] data) {
            this._entries[id] = data;
        }

        public PakFile Build(string filePath) {
            using var ms = new MemoryStream();
            using var bw = new BinaryWriter(ms);

            bw.Write(this._entries.Count);
            foreach (var entry in this._entries) {
                bw.Write(entry.Key);
                bw.Write(entry.Value.Length);
                bw.Write(entry.Value);
            }

            var fi = new FileInfo(filePath);
            if (!fi.Directory.Exists) {
                Directory.CreateDirectory(fi.Directory.FullName);
            }

            File.WriteAllBytes(filePath, ms.ToArray());
            return new PakFile(filePath);
        }
    }
}
