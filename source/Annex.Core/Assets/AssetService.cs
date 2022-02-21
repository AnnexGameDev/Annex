using Annex.Core.Assets.Bundles;

namespace Annex.Core.Assets
{
    internal class AssetService : IAssetService
    {
        public IAssetGroup Textures { get; }

        public AssetService() {
            Textures = new AssetGroup();
        }
    }

    // TODO: Tests
    // TODO: Clean this up
    internal class AssetGroup : IAssetGroup
    {
        private readonly List<IAssetBundle> _bundles = new();

        public void AddBundle(IAssetBundle bundle) {
            this._bundles.Add(bundle);
        }

        public IAsset Get(string textureId) {
            return this._bundles
                .Select(bundle => bundle.GetAsset(textureId))
                .Where(asset => asset != null)
                .FirstOrDefault()!;
        }
    }
}