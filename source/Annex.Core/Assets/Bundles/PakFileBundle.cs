using Scaffold.Logging;
using System.Collections.Concurrent;
using System.Text;

namespace Annex.Core.Assets.Bundles
{
    // TODO: Tests
    // TODO: Clean this up
    public class PakFileBundle : IAssetBundle
    {
        private readonly IDictionary<string, IAsset> _assets = new ConcurrentDictionary<string, IAsset>();
        private readonly PakFile _pakFile;

        public string Id { get; }

        /// <remarks>
        /// Use the assetroot if you want to copy files from the root (this is usually used for version control), and not for production
        /// </remarks>
        public PakFileBundle(string pakFilePath, string fileFilter, string? assetRoot = null, string bundleId = "") {

            Id = bundleId;

            if (!string.IsNullOrWhiteSpace(bundleId))
            {
                assetRoot += "\\" + bundleId;
            }

            if (assetRoot is null)
            {
                this._pakFile = new PakFile(pakFilePath);
                return;
            }

            Log.Verbose($"Constructing the pak file {pakFilePath}...");
            this._pakFile = PakFile.CreateFrom(pakFilePath, fileFilter, assetRoot)!;
        }

        public IAsset? GetAsset(string id) {
            id = id.ToSafeAssetIdString();

            if (this._assets.ContainsKey(id))
            {
                return this._assets[id];
            }

            if (!this._pakFile.TryGetEntry(id, out var entry))
            {
                return null;
            }

            this._assets.Add(id, new PakFileAsset(id, entry));
            return this._assets[id];
        }

        public void Dispose() {
            this._pakFile.Dispose();
        }

        public IEnumerable<IAsset> GetAssets() {
            return _pakFile.AssetIds.Select(assetId => GetAsset(assetId));
        }

        public IEnumerable<IAsset> GetAssets(Predicate<IAsset> predicate) {
            return GetAssets().Where(asset => predicate(asset));
        }

        private class PakFile : IDisposable
        {
            private readonly Dictionary<string, PakFileEntry> _entries = new();

            public IEnumerable<string> AssetIds => _entries.Keys;

            private readonly FileStream _fileStream;
            private readonly BufferedStream _bufferedStream;
            private readonly BinaryReader _reader;

            public PakFile(string filePath) {
                this._fileStream = new FileStream(filePath, FileMode.Open);
                this._bufferedStream = new BufferedStream(this._fileStream);
                this._reader = new BinaryReader(this._bufferedStream);

                int numAssets = this._reader.ReadInt32();
                for (int i = 0; i < numAssets; i++)
                {
                    string assetId = this._reader.ReadString();
                    int assetSize = this._reader.ReadInt32();
                    long assetPosition = this._reader.BaseStream.Position;
                    this._reader.BaseStream.Seek(assetSize, SeekOrigin.Current);
                    this._entries.Add(assetId, new PakFileEntry(assetPosition, assetSize));
                }
            }

            public static PakFile? CreateFrom(string pakFilePath, string fileFilter, string assetRoot) {

                if (File.Exists(pakFilePath))
                {
                    Log.Verbose($"Deleting old pakFile {pakFilePath}");
                    File.Delete(pakFilePath);
                }

                using var ms = new MemoryStream();
                using var bw = new BinaryWriter(ms);

                var allAssets = Directory.GetFiles(assetRoot, fileFilter, SearchOption.AllDirectories);
                bw.Write(allAssets.Count());

                foreach (var asset in allAssets)
                {
                    var fi = new FileInfo(asset);
                    string assetId = fi.FullName.Remove(0, assetRoot.Length + 1).ToSafeAssetIdString();
                    var assetData = File.ReadAllBytes(asset);

                    Log.Verbose($"Adding '{assetId}' to pakFile");
                    bw.Write(assetId);
                    bw.Write(assetData.Length);
                    bw.Write(assetData);
                }

                File.WriteAllBytes(pakFilePath, ms.ToArray());
                return new PakFile(pakFilePath);
            }

            public void Dispose() {
                this._reader.Close();
                this._reader.Dispose();

                this._bufferedStream.Close();
                this._bufferedStream.Dispose();

                this._fileStream.Close();
                this._fileStream.Dispose();
            }

            public bool TryGetEntry(string id, out PakFileEntry entry) {
                entry = default;

                if (!this._entries.ContainsKey(id))
                {
                    Log.Error($"Tried to get pakfileentry {id} which doesn't exist");
                    return false;
                }
                entry = this._entries[id];

                if (!entry.HasData)
                {
                    lock (this._reader)
                    {
                        this._reader.BaseStream.Seek(entry.Position, SeekOrigin.Begin);
                        entry.SetData(this._reader.ReadBytes(entry.Size));
                    }
                }

                return true;
            }

            public class PakFileEntry
            {
                public readonly long Position;
                public readonly int Size;
                public byte[]? Data { get; private set; }
                public bool HasData => Data != null;

                public PakFileEntry(long position, int size) {
                    this.Position = position;
                    this.Size = size;
                }

                public void SetData(byte[] data) {
                    if (HasData)
                        throw new InvalidOperationException("Tried to set data to a pakfileentry with existing data");
                    Data = data;
                }
            }
        }

        private class PakFileAsset : Asset
        {
            private readonly byte[] _data;
            public override bool FilepathSupported => false;
            public override string FilePath => throw new NotSupportedException();

            public PakFileAsset(string id, PakFile.PakFileEntry entry) : base(id) {
                this._data = entry.Data!;
            }

            public override byte[] ToBytes() {
                return this._data;
            }

            public override string ToString() {
                return Encoding.UTF8.GetString(this._data);
            }
        }
    }
}