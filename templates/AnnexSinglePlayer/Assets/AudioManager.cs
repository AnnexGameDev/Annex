using Annex.Assets;
using Annex.Assets.Loaders;
using Annex.Assets.Services;

namespace AnnexSinglePlayer.Assets
{
    public class AudioManager : AssetManager, IAudioManager
    {
        public AudioManager() : base(new FileSystemStreamer("audio", "*.wav", ".flac")) {
        }
    }
}
