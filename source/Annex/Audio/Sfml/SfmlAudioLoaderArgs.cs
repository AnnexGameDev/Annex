using Annex.Resources;

namespace Annex.Audio.Sfml
{
    public class SfmlAudioLoaderArgs : IResourceLoaderArgs
    {
        public string Key { get; set; }
        public BufferMode BufferMode { get; set; }

        public SfmlAudioLoaderArgs(string key, BufferMode bufferMode) {
            this.Key = key;
            this.BufferMode = bufferMode;
        }
    }
}
