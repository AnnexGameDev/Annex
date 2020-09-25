using Annex.Services;
using System.Collections.Generic;

namespace Annex.Audio
{
    public interface IAudioService : IService
    {
        IPlayingAudio PlayAudio(string audioFilePath);
        IPlayingAudio PlayAudio(string audioFilePath, AudioContext context);
        void StopPlayingAudio(string? id = null);
        IEnumerable<IPlayingAudio> GetPlayingAudio(string? id = null);
    }
}
