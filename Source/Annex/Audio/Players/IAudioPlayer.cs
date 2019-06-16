namespace Annex.Audio.Players
{
    public interface IAudioPlayer
    {
        void PlaySound(string name, bool loop = false, float volume = 100.0f);
        void PlayMusic(string name, bool loop = false, float volume = 100.0f);

        void StopAllMusic();
        void StopAllSound();
    }
}
