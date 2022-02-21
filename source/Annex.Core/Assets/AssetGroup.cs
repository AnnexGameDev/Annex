using Annex.Core.Assets.Bundles;

namespace Annex.Core.Assets
{
    internal class AssetGroup : IAssetGroup
    {
        private readonly List<IAssetBundle> _bundles = new();

        public void AddBundle(IAssetBundle bundle) {
            this._bundles.Add(bundle);
        }

        public IAsset GetAsset(string assetId) {

            var bundles = this._bundles
                .Select(bundle => bundle.GetAsset(assetId))
                .Where(asset => asset != null);

            if (bundles.Count() != 1) {
                throw new AggregateException($"Unable to find asset {assetId}");
            }

            return bundles.Single()!;
        }
    }
}