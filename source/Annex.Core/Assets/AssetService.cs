namespace Annex.Core.Assets
{
    internal class AssetService : IAssetService
    {
        public ITextures Textures { get; }

        public AssetService(ITextures textures) {
            this.Textures = textures;
        }
    }
}