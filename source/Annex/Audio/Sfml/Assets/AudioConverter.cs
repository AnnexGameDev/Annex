using Annex.Assets;
using Annex.Assets.Converters;

namespace Annex.Audio.Sfml.Assets
{
    public class AudioConverter : IAssetConverter
    {
        public Asset CreateAsset(string id, byte[] assetData) {
            return new AudioAsset(id, assetData);
        }

        public bool Validate(Asset asset) {
            return asset is AudioAsset;
        }
    }
}
