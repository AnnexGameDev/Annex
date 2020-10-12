using Annex.Assets.Converters;
using Annex.Assets.Streams;
using System.Collections.Generic;
using System.Linq;

namespace Annex.Assets
{
    public abstract class AssetManager : IAssetManager
    {
        public IDataStreamer DataStreamer { get; }

        private readonly Dictionary<string, Asset> _cache;

        public AssetManager(IDataStreamer streamer) {
            this.DataStreamer = streamer;
            this._cache = new Dictionary<string, Asset>();
        }

        public void Destroy() {
            foreach (var asset in this._cache.Values) {
                asset.Dispose();
            }
            this._cache.Clear();
        }

        // TODO: This never returns false
        public bool GetAsset(AssetConverterArgs args, out Asset asset) {
            if (this._cache.ContainsKey(args.Id)) {
                Asset cachedAsset = this._cache[args.Id];
                if (args.Converter.Validate(cachedAsset)) {
                    asset = cachedAsset;
                    return true;
                }
                this.UnloadCachedAsset(args.Id);
            }
            var data = this.DataStreamer.Read(args.Id);
            asset = args.Converter.CreateAsset(args.Id, data);
            this._cache.Add(args.Id, asset);
            return true;
        }

        public Asset[] GetCachedAssets() {
            return this._cache.Values.ToArray();
        }

        public void UnloadCachedAsset(string id) {
            if (this._cache.ContainsKey(id)) {
                var asset = this._cache[id];
                asset.Dispose();
                this._cache.Remove(id);
            }
        }
    }
}
