using Annex.Core.Assets.Bundles;
using Scaffold.Logging;

namespace Annex.Core.Assets
{
    public class AssetGroup : IAssetGroup
    {
        private readonly List<IAssetBundle> _bundles = new();

        public string Id { get; }

        public AssetGroup(string id) {
            this.Id = id;
        }

        public void AddBundle(IAssetBundle bundle) {
            this._bundles.Add(bundle);
        }

        public IAsset? GetAsset(string assetId) {

            var bundles = this._bundles
                .Select(bundle => bundle.GetAsset(assetId))
                .Where(asset => asset != null);

            if (bundles.Count() != 1) {
                Log.Trace(LogSeverity.Error, $"Unable to find asset {assetId}");
                return null;
            }

            return bundles.Single()!;
        }
    }
}