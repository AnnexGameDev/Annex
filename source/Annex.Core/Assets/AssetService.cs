namespace Annex.Core.Assets
{
    internal class AssetService : IAssetService
    {
        public IAssetGroup Textures { get; }

        public AssetService(IAssetGroup textures) {
            this.Textures = textures;
        }
    }
}