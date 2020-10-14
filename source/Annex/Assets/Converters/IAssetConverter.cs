namespace Annex.Assets.Converters
{
    public interface IAssetConverter
    {
        Asset CreateAsset(string id, byte[] assetData);
        bool Validate(Asset asset);
    }
}
