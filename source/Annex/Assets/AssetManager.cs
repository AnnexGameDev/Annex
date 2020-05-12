#nullable enable

namespace Annex.Assets
{
    public abstract class AssetManager : IAssetManager
    {
        public readonly IAssetLoader DataLoader;
        public readonly IAssetInitializer AssetLoader;

        public AssetManager(IAssetLoader dataLoader, IAssetInitializer assetLoader) {
            this.DataLoader = dataLoader;
            this.AssetLoader = assetLoader;
        }

        public bool GetAsset(IAssetInitializerArgs args, out object? asset) {
            if (!this.AssetLoader.Validate(args)) {
                ServiceProvider.Log.WriteLineWarning($"The asset '{args}' is not valid");
                asset = default;
                return false;
            }

            if (!this.ContainsCachedAsset(args.Key)) {
                var loadedAsset = this.AssetLoader.Load(args, this.DataLoader);
                Debug.ErrorIf(loadedAsset == null, $"Loaded asset {args.Key} is null");
#pragma warning disable CS8604 // Possible null reference argument.
                this.CacheAsset(args.Key, loadedAsset);
#pragma warning restore CS8604 // Possible null reference argument.
            }

            asset = this.RetrieveCachedAsset(args.Key);
            return true;
        }

        protected abstract object RetrieveCachedAsset(string key);
        protected abstract void CacheAsset(string key, object asset);
        protected abstract bool ContainsCachedAsset(string key);
    }
}
