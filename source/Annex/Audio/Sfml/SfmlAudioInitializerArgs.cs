using Annex.Assets;

namespace Annex.Audio.Sfml
{
    public class SfmlAudioInitializerArgs : AssetInitializerArgs
    {
        public BufferMode BufferMode { get; set; }

        public SfmlAudioInitializerArgs(string key, BufferMode bufferMode) : base(key) {
            this.BufferMode = bufferMode;
        }
    }
}
