using Annex.Services;
using System.IO;
using static Annex.Assets.Errors;

namespace Annex.Assets
{
    public abstract class AssetManager : IAssetManager
    {
        public readonly IAssetLoader AssetLoader;
        public readonly IAssetInitializer AssetInitializer;
        public AssetType AssetType { get; set; }

        public AssetManager(AssetType type, IAssetLoader assetLoader, IAssetInitializer assetInitializer) {
            this.AssetType = type;
            this.AssetLoader = assetLoader;
            this.AssetInitializer = assetInitializer;
        }

        public bool GetAsset(AssetInitializerArgs args, out object? asset) {

            if (!this.AssetInitializer.Validate(args)) {
                ServiceProvider.Log.WriteLineWarning(ASSET_NOT_VALID.Format(args.Key));
                asset = default;
                return false;
            }

            if (!this.ContainsCachedAsset(args.Key)) {
                var loadedAsset = this.AssetInitializer.Load(args, this.AssetLoader);
                Debug.ErrorIf(loadedAsset == null, ASSET_IS_NULL.Format(args.Key));
#pragma warning disable CS8604 // Possible null reference argument.
                this.CacheAsset(args.Key, loadedAsset);
#pragma warning restore CS8604 // Possible null reference argument.
            }

            asset = this.RetrieveCachedAsset(args.Key);
            return true;
        }

        public void PackageAssetsToBinaryFrom(string path) {

            Directory.CreateDirectory(path);

            foreach (var file in Directory.GetFiles(path, "*", SearchOption.AllDirectories)) {
                string relativePath = file.Substring(path.Length);
                this.AssetLoader.Write(Path.Combine(path, relativePath), Path.Combine(this.AssetInitializer.AssetPath, relativePath));
            }
        }

        protected abstract object RetrieveCachedAsset(string key);
        protected abstract void CacheAsset(string key, object asset);
        protected abstract bool ContainsCachedAsset(string key);

    }
}
