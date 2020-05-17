using System.Collections.Generic;

namespace Annex.Audio
{
    public interface IAudioPlayer : IService
    {
        IPlayingAudio PlayAudio(string audioFilePath);
        IPlayingAudio PlayAudio(string audioFilePath, AudioContext context);
        void StopPlayingAudio(string? id = null);
        IEnumerable<IPlayingAudio> GetPlayingAudio(string? id = null);
    }
}
