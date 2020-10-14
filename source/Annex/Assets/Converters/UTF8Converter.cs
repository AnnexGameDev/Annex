using System.Text;

namespace Annex.Assets.Converters
{
    public class UTF8Converter : IAssetConverter
    {
        public Asset CreateAsset(string id, byte[] assetData) {
            return new UTF8Asset(id, Encoding.UTF8.GetString(assetData));
        }

        public bool Validate(Asset asset) {
            return asset is UTF8Asset;
        }
    }
}
