using Annex.Events;
using SFML.Audio;
using System.Collections.Generic;

namespace Annex.Audio.Players.Sfml
{
    internal sealed class SfmlPlayer : IAudioPlayer
    {
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
            }, 5000);
        }

        public void StopAllAudio() {
            lock (this._lock) {
                foreach (var audio in this._playingAudio) {
                    audio.Stop();
                    audio.Dispose();
                }
                this._playingAudio.Clear();
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

        public void StopById(string id) {
            lock (this._lock) {
                for (int i = 0; i < this._playingAudio.Count; i++) {
                    var audio = this._playingAudio[i];
                    if (audio.Id == id) {
                        audio.Stop();
                        audio.Dispose();
                        this._playingAudio.RemoveAt(i--);
                    }
                }
            }
        }
    }
}
