namespace Annex.Core.Assets
{
    internal class AssetService : IAssetService
    {
        public IAssetGroup Textures { get; }
        public IAssetGroup Fonts { get; }
        public IAssetGroup SceneData { get; }

        public AssetService(IAssetGroup textures, IAssetGroup fonts, IAssetGroup sceneData) {
            this.Textures = textures;
            this.Fonts = fonts;
            this.SceneData = sceneData;
        }
    }
}