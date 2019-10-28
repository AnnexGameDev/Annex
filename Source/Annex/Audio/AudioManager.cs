using Annex.Audio.Players;
using Annex.Audio.Players.Sfml;

namespace Annex.Audio
{
    public sealed class AudioManager : Singleton
    {
        private readonly IAudioPlayer _player;

        static AudioManager() {
            Create<AudioManager>();
        }
        public static IAudioPlayer Singleton => Get<AudioManager>()._player;

        public AudioManager() {
            this._player = new SfmlPlayer();
        }
    }
}
