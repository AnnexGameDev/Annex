namespace SampleProject.Assets
{
    public class AudioManager : AssetManager, IAudioManager
    {
        public AudioManager() : base(new FileSystemStreamer("audio", "*.wav", ".flac")) {
        }
    }
}
