namespace Annex_Old.Core.Assets
{
    public interface IAssetService
    {
        public IAssetGroup Textures { get; }
        public IAssetGroup Fonts { get; }
        public IAssetGroup SceneData { get; }
    }
}