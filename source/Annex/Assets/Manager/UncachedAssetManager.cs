#nullable enable
using System.Collections.Generic;
using System.IO;

namespace Annex.Assets.Managers
{
    public class UncachedAssetManager : AssetManager
    {
        private Dictionary<string, object> _temp_cache;

        public UncachedAssetManager(AssetType type, IAssetLoader assetLoader, IAssetInitializer assetInitializer) 
            : base(type, assetLoader, assetInitializer) {
            this._temp_cache = new Dictionary<string, object>();
        }

        protected override void CacheAsset(string key, object asset) {
            this._temp_cache[key] = asset;
        }

        protected override bool ContainsCachedAsset(string key) {
            return false;
        }

        protected override object RetrieveCachedAsset(string key) {
            var asset = this._temp_cache[key];
            this._temp_cache.Remove(key);
            return asset;
        }
    }
}
