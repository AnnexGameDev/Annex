namespace Annex.Core.Assets.Bundles
{
    public interface IAssetBundle : IDisposable
    {
        string Id { get; }

        IAsset? GetAsset(string id);
        IEnumerable<IAsset> GetAssets();
        IEnumerable<IAsset> GetAssets(Predicate<IAsset> predicate);
    }
}