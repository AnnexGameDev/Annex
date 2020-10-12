using Annex.Assets.Converters;

namespace Annex.Audio.Sfml.Assets
{
    public class SfmlAudioConverterArgs : AssetConverterArgs
    {
        private static BufferedAudioConverter _converterBuffered = new BufferedAudioConverter();
        private static BufferedAudioConverter _converterUnbuffered = new BufferedAudioConverter();

        public static IAssetConverter GetTargetConverter(BufferMode mode) {
            return mode switch
            {
                BufferMode.Buffered => _converterBuffered,
                BufferMode.None => _converterUnbuffered,
                _ => throw new System.NotImplementedException()
            };
        }

        public SfmlAudioConverterArgs(string key, BufferMode bufferMode) : base(key, GetTargetConverter(bufferMode)) {
        }
    }
}
