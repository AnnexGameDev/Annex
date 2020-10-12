using Annex.Assets;
using Annex.Assets.Converters;

namespace Annex.Graphics.Sfml.Assets
{
    public class TextureConverter : IAssetConverter
    {
        public Asset CreateAsset(string id, byte[] assetData) {
            return new TextureAsset(id, assetData);
        }

        public bool Validate(Asset asset) {
            return asset is TextureAsset;
        }
    }
}
