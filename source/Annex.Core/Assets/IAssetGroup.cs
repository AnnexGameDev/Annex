using Annex.Core.Assets.Bundles;

namespace Annex.Core.Assets
{
    public interface IAssetGroup
    {
        string Id { get; }

        void AddBundle(IAssetBundle bundle);

        IAsset? GetAsset(string assetId);
    }
}
