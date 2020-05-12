using Annex.Assets;

namespace Annex.Audio.Sfml
{
    public class SfmlAudioInitializerArgs : IAssetInitializerArgs
    {
        public string Key { get; set; }
        public BufferMode BufferMode { get; set; }

        public SfmlAudioInitializerArgs(string key, BufferMode bufferMode) {
            this.Key = key;
            this.BufferMode = bufferMode;
        }
    }
}
