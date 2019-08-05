using Annex.Events;
using Annex.Resources;
using SFML.Audio;
using System.Collections.Generic;

namespace Annex.Audio.Players.Sfml
{
    public class SfmlPlayer : IAudioPlayer
    {
        private List<Music> _playingMusic;
        private List<Sound> _playingSounds;

        private object _lock = new object();

        public SfmlPlayer() {
            this._playingSounds = new List<Sound>();
            this._playingMusic = new List<Music>();

            GameEvents.Singleton.AddEvent(PriorityType.SOUNDS, () => {
                lock (this._lock) {
                    for (int i = 0; i < this._playingMusic.Count; i++) {
                        var music = this._playingMusic[i];
                        if (music.Status == SoundStatus.Stopped) {
                            music.Stop();
                            music.Dispose();
                            this._playingMusic.RemoveAt(i--);
                        }
                    }
                    for (int i = 0; i < this._playingSounds.Count; i++) {
                        var sound = this._playingSounds[i];
                        if (sound.Status == SoundStatus.Stopped) {
                            sound.Stop();
                            sound.Dispose();
                            this._playingSounds.RemoveAt(i--);
                        }
                    }
                    return ControlEvent.NONE;
                }
            }, 5000);
        }

        public void PlayMusic(string name, bool loop = false, float volume = 100) {
            lock (this._lock) {
                var music = new Music("resources/audio/" + name) {
                    Loop = loop,
                    Volume = volume
                };
                music.Play();
                this._playingMusic.Add(music);
            }
        }

        public void PlaySound(string name, bool loop = false, float volume = 100) {
            lock (this._lock) {
                var sound = new Sound(new SoundBuffer("resources/audio/" + name)) {
                    Loop = false,
                    Volume = volume
                };
                sound.Play();
                this._playingSounds.Add(sound);
            }
        }

        public void StopAllMusic() {
            for (int i = 0; i < this._playingMusic.Count; i++) {
                var music = this._playingMusic[i];
                music.Stop();
                music.Dispose();
                this._playingMusic.RemoveAt(i--);
            }
        }

        public void StopAllSound() {
            for (int i = 0; i < this._playingSounds.Count; i++) {
                var sound = this._playingSounds[i];
                sound.Stop();
                sound.Dispose();
                this._playingSounds.RemoveAt(i--);
            }
        }
    }
}
