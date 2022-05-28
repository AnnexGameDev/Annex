using Annex_Old.Core.Assets.Bundles;

namespace Annex_Old.Core.Assets
{
    public interface IAssetGroup
    {
        void AddBundle(IAssetBundle bundle);

        IAsset? GetAsset(string assetId);
    }
}
