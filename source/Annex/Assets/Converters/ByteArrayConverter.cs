namespace Annex_Old.Assets.Converters
{
    public class ByteArrayConverter : IAssetConverter
    {
        public Asset CreateAsset(string id, byte[] assetData) {
            return new ByteArrayAsset(id, assetData);
        }

        public bool Validate(Asset asset) {
            return asset is ByteArrayAsset;
        }
    }
}
