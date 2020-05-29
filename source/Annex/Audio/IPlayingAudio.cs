using System;

namespace Annex.Audio
{
    public interface IPlayingAudio : IDisposable
    {
        bool IsPlaying { get; }
        string ID { get; }
        float Volume { get; set; }
    }
}
