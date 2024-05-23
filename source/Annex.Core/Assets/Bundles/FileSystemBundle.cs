using Scaffold.Logging;
using System.Collections.Concurrent;

namespace Annex.Core.Assets.Bundles
{
    // TODO: Tests
    // TODO: Clean this up
    public class FileSystemBundle : IAssetBundle
    {
        public string Id { get; }

        private readonly IDictionary<string, IAsset> _assets = new ConcurrentDictionary<string, IAsset>();

        public FileSystemBundle(string filter, string rootPath, string bundleId = "") {

            Id = bundleId;

            if (!string.IsNullOrWhiteSpace(bundleId))
            {
                rootPath = bundleId + "/" + rootPath;
            }

            foreach (var file in Directory.GetFiles(rootPath, filter, SearchOption.AllDirectories))
            {
                var fi = new FileInfo(file);
                var assetId = fi.FullName.Remove(0, rootPath.Length + 1).ToSafeAssetIdString();
                Log.Trace(LogSeverity.Verbose, $"Adding asset {assetId}");
                this._assets.Add(assetId, new FileAsset(assetId, fi.FullName));
            }
        }

        public IAsset? GetAsset(string id) {
            id = id.ToSafeAssetIdString();
            if (!this._assets.ContainsKey(id))
            {
                return null;
            }

            return this._assets[id];
        }

        private class FileAsset : Asset
        {
            public override bool FilepathSupported => true;
            public override string FilePath { get; }

            public FileAsset(string id, string fullName) : base(id) {
                this.FilePath = fullName;
            }

            public override byte[] ToBytes() {
                return File.ReadAllBytes(this.FilePath);
            }

            public override string ToString() {
                return File.ReadAllText(this.FilePath);
            }
        }

        public void Dispose() {
        }

        public IEnumerable<IAsset> GetAssets() {
            return _assets.Values;
        }

        public IEnumerable<IAsset> GetAssets(Predicate<IAsset> predicate) {
            return _assets.Values.Where(asset => predicate(asset));
        }
    }
}