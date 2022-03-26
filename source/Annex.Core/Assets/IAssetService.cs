namespace Annex.Core.Assets
{
    public interface IAssetService
    {
        public IAssetGroup Textures { get; }
        public IAssetGroup Fonts { get; }
    }
}