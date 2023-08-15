namespace Annex.Core.Assets
{
    public interface IAssetService
    {
        bool TryGetAssetGroup(string id, out IAssetGroup assetGroup);
    }
}