#nullable enable

namespace Annex.Audio
{
    public interface IAudioPlayer : IService
    {
        void PlayBufferedAudio(string name, string id = "", bool loop = false, float volume = 100);
        void PlayAudio(string name, string id = "", bool loop = false, float volume = 100);
        void StopAllAudio(string? id = null);
    }
}
