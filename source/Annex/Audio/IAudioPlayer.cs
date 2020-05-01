#nullable enable

namespace Annex.Audio
{
    public interface IAudioPlayer : IService
    {
        void PlayAudio(string audioFilePath);
        void PlayAudio(string audioFilePath, AudioContext context);
        void StopAudio(string? id = null);
    }
}
