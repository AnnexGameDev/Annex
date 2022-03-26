namespace Annex.Core.Assets
{
    internal class AssetService : IAssetService
    {
        public IAssetGroup Textures { get; }
        public IAssetGroup Fonts { get; }

        public AssetService(IAssetGroup textures, IAssetGroup fonts) {
            this.Textures = textures;
            this.Fonts = fonts;
        }
    }
}