using Scaffold.Logging;
using System.Collections.Concurrent;

namespace Annex.Core.Assets.Bundles
{
    // TODO: Tests
    // TODO: Clean this up
    public class FileSystemBundle : IAssetBundle
    {
        private readonly IDictionary<string, IAsset> _assets = new ConcurrentDictionary<string, IAsset>();

        public FileSystemBundle(string filter, string rootPath) {
            foreach (var file in Directory.GetFiles(rootPath, filter, SearchOption.AllDirectories)) {
                var fi = new FileInfo(file);
                var assetId = fi.FullName.Remove(0, rootPath.Length + 1).ToSafeAssetIdString();
                Log.Trace(LogSeverity.Verbose, $"Adding asset {assetId}");
                this._assets.Add(assetId, new FileAsset(fi.FullName));
            }
        }

        public IAsset? GetAsset(string id) {
            id = id.ToSafeAssetIdString();
            if (!this._assets.ContainsKey(id)) {
                return null;
            }

            return this._assets[id];
        }

        private class FileAsset : Asset
        {
            public override bool FilepathSupported => true;
            public override string FilePath { get; }

            public FileAsset(string fullName) {
                this.FilePath = fullName;
            }

            public override byte[] ToBytes() {
                return File.ReadAllBytes(this.FilePath);
            }
        }

        public void Dispose() {
        }
    }
}