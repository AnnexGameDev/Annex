#nullable enable

namespace Annex.Audio.Players
{
    public interface IAudioPlayer
    {
        void PlayBufferedAudio(string name, string id = "", bool loop = false, float volume = 100);
        void PlayAudio(string name, string id = "", bool loop = false, float volume = 100);
        void StopAllAudio(string? id = null);
    }
}
