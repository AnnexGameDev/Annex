using Annex.Assets;
using Annex.Assets.Converters;

namespace Annex.Graphics.Sfml.Assets
{
    public class IconConverter : IAssetConverter
    {
        public Asset CreateAsset(string id, byte[] assetData) {
            return new IconAsset(id, assetData);
        }

        public bool Validate(Asset asset) {
            return asset is IconAsset;
        }
    }
}
