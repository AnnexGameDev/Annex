using Annex.Assets;
using Annex.Assets.Streams;
using Annex.Assets.Services;

namespace SampleProject.Assets
{
    public class AudioManager : AssetManager, IAudioManager
    {
        public AudioManager() : base(new FileSystemStreamer("audio", "*.wav", ".flac")) {
        }
    }
}
