using Annex.Core.Assets.Bundles;

namespace Annex.Core.Assets
{
    public interface IAssetGroup
    {
        void AddBundle(IAssetBundle bundle);
        IAsset Get(string textureId);
    }
}
