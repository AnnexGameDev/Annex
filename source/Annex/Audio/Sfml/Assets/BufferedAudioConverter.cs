using Annex.Assets;
using Annex.Assets.Converters;

namespace Annex.Audio.Sfml.Assets
{
    public class BufferedAudioConverter : IAssetConverter
    {
        public bool Validate(Asset asset) {
            return asset is BufferedAudioAsset;
        }

        Asset IAssetConverter.CreateAsset(string id, byte[] assetData) {
            return new BufferedAudioAsset(id, assetData);
        }
    }
}