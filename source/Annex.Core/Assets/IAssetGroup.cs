using Annex.Core.Assets.Bundles;

namespace Annex.Core.Assets
{
    public interface IAssetGroup
    {
        string Id { get; }

        void AddBundle(IAssetBundle bundle);
        IAssetBundle? GetBundle(string id);

        IAsset? GetAsset(string assetId);
        IEnumerable<IAsset> GetAssets();
        IEnumerable<IAsset> GetAssets(Predicate<IAsset> predicate);
    }
}
