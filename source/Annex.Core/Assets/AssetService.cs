namespace Annex.Core.Assets
{
    internal class AssetService : IAssetService
    {
        private readonly IEnumerable<IAssetGroup> _assetGroups;

        public AssetService(IEnumerable<IAssetGroup> assetGroups) {
            this._assetGroups = assetGroups;
        }

        public bool TryGetAssetGroup(string id, out IAssetGroup assetGroup) {
            assetGroup = this._assetGroups.FirstOrDefault(group => group.Id == id);
            return assetGroup != null;
        }
    }
}