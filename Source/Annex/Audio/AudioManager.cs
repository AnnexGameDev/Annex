using Annex.Audio.Players;
using Annex.Audio.Players.Sfml;

namespace Annex.Audio
{
    public class AudioManager : Singleton
    {
        private IAudioPlayer _player;

        static AudioManager() {
            Create<AudioManager>();
        }
        public static IAudioPlayer Singleton => Get<AudioManager>()._player;

        public AudioManager() {
            this._player = new SfmlPlayer();
        }
    }
}
