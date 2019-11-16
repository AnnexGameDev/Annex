#nullable enable
using Annex.Events;
using SFML.Audio;
using System.Collections.Generic;

namespace Annex.Audio.Players.Sfml
{
    internal sealed class SfmlPlayer : IAudioPlayer
    {
        public const string GameEventID = "sfml-audio-player-gc";
        private readonly List<PlayingAudio> _playingAudio;
        private readonly object _lock = new object();

        internal SfmlPlayer() {
            this._playingAudio = new List<PlayingAudio>();

            EventManager.Singleton.AddEvent(PriorityType.SOUNDS, () => {
                lock (this._lock) {
                    for (int i = 0; i < this._playingAudio.Count; i++) {
                        var audio = this._playingAudio[i];
                        if (audio.IsStopped()) {
                            audio.Stop();
                            audio.Dispose();
                            this._playingAudio.RemoveAt(i--);
                        }
                    }
                    return ControlEvent.NONE;
                }
            }, 5000, eventID: GameEventID);
        }

        public void StopAllAudio(string? id = null) {
            lock (this._lock) {
                for (int i = 0; i < this._playingAudio.Count; i++) {
                    var audio = this._playingAudio[i];
                    if (id != null && audio.Id != id) {
                        continue;
                    }
                    audio.Stop();
                    audio.Dispose();
                    this._playingAudio.RemoveAt(i--);
                }
            }
        }

        public void PlayBufferedAudio(string name, string id, bool loop, float volume) {
            lock (this._lock) {
                var music = new Music("resources/audio/" + name) {
                    Loop = loop,
                    Volume = volume
                };
                music.Play();
                this._playingAudio.Add(new PlayingAudio(id, music));
            }
        }

        public void PlayAudio(string name, string id, bool loop, float volume) {
            lock (this._lock) {
                var sound = new Sound(new SoundBuffer("resources/audio/" + name)) {
                    Loop = false,
                    Volume = volume
                };
                sound.Play();
                this._playingAudio.Add(new PlayingAudio(id, sound));
            }
        }
    }
}
