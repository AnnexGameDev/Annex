namespace Annex.Core.Assets.Bundles
{
    public interface IAssetBundle : IDisposable
    {
        public IAsset? GetAsset(string id);
    }
}