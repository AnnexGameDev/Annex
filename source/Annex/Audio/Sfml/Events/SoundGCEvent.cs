using Annex.Events;
using System.Collections.Generic;

namespace Annex.Audio.Sfml.Events
{
    internal class SoundGCEvent : GameEvent
    {
        internal const string GameEventID = "sfml-audio-player-gc";
        private readonly object _lock;
        private readonly List<SfmlPlayingAudio> _playingAudio;

        internal SoundGCEvent(object sfmlPlayerLock, List<SfmlPlayingAudio> playingAudio) : base(GameEventID, 5000, 0) {
            this._lock = sfmlPlayerLock;
            this._playingAudio = playingAudio;
        }

        protected override void Run(EventArgs gameEventArgs) {
            lock (this._lock) {
                for (int i = 0; i < this._playingAudio.Count; i++) {
                    var audio = this._playingAudio[i];
                    if (audio.IsStopped) {
                        audio.Stop();
                        audio?.Dispose();
                        this._playingAudio.RemoveAt(i--);
                    }
                }
            }
        }
    }
}
