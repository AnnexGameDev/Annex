#nullable enable
using Annex.Assets.Loaders;
using System.Collections.Generic;

namespace Annex.Assets.Managers
{
    public class PakAssetManager : AssetManager
    {
        private readonly Dictionary<string, object> _assets;

        public PakAssetManager(string pakFilePath, IAssetInitializer assetLoader)
            : this(new PakLoader(PakFile.CreateForInput(pakFilePath)), assetLoader) {
        }

        public PakAssetManager(IAssetLoader dataLoader, IAssetInitializer assetLoader)
            : base(dataLoader, assetLoader) {
            this._assets = new Dictionary<string, object>();
        }

        protected override void CacheAsset(string key, object asset) {
            this._assets[key] = asset;
        }

        protected override bool ContainsCachedAsset(string key) {
            return this._assets.ContainsKey(key);
        }

        protected override object RetrieveCachedAsset(string key) {
            return this._assets[key];
        }
    }
}
