namespace Annex_Old.Core.Assets.Bundles
{
    public interface IAssetBundle : IDisposable
    {
        public IAsset? GetAsset(string id);
    }
}