using Annex_Old.Services;
using System.Collections.Generic;

namespace Annex_Old.Audio
{
    public interface IAudioService : IService
    {
        IPlayingAudio PlayAudio(string audioFilePath);
        IPlayingAudio PlayAudio(string audioFilePath, AudioContext context);
        void StopPlayingAudio(string? id = null);
        IEnumerable<IPlayingAudio> GetPlayingAudio(string? id = null);
    }
}
