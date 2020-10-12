using Annex.Assets;
using Annex.Assets.Converters;

namespace Annex.Audio.Sfml.Assets
{
    public class UnbufferedAudioConverter : IAssetConverter
    {
        public Asset CreateAsset(string id, byte[] assetData) {
            return new UnbufferedAudioAsset(id, assetData);
        }

        public bool Validate(Asset asset) {
            return asset is UnbufferedAudioAsset;
        }
    }
}
