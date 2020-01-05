#nullable enable
using Annex.Events;
using Annex.Resources;
using Annex.Resources.FS;
using SFML.Audio;
using System;
using System.Collections.Generic;
using System.IO;

namespace Annex.Audio.Sfml
{
    internal sealed class SfmlPlayer : IAudioPlayer
    {
        public const string GameEventID = "sfml-audio-player-gc";
        private readonly List<PlayingAudio> _playingAudio;
        private readonly object _lock = new object();

        private readonly string AudioPath = Path.Combine(AppContext.BaseDirectory, "audio/");
        private string AudioLoader_String(string path) => path;
        private byte[] AudioLoader_Bytes(byte[] data) => data;
        private bool AudioValidator(string path) => path.EndsWith(".flac") || path.EndsWith(".wav");

        public SfmlPlayer() {
            this._playingAudio = new List<PlayingAudio>();

            var resources = ServiceProvider.ResourceManagerRegistry;
            var audio = resources.GetOrCreateResourceManager<FSResourceManager>(ResourceType.Audio);
            audio.SetResourcePath(this.AudioPath);
            audio.SetResourceLoader(this.AudioLoader_Bytes);
            audio.SetResourceLoader(this.AudioLoader_String);
            audio.SetResourceValidator(this.AudioValidator);

            ServiceProvider.EventManager.AddEvent(PriorityType.SOUNDS, () => {
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
            name = name.ToLower();
            lock (this._lock) {
                var audio = ServiceProvider.ResourceManagerRegistry.GetResourceManager(ResourceType.Audio);
                var resource = audio.GetResource(name);
                Music? music = null;
                if (resource is string path) {
                    music = new Music(path) {
                        Loop = loop,
                        Volume = volume
                    };
                }
                if (resource is byte[] data) {
                    music = new Music(data) {
                        Loop = loop,
                        Volume = volume
                    };
                }
                music.Play();
                this._playingAudio.Add(new PlayingAudio(id, music));
            }
        }

        public void PlayAudio(string name, string id, bool loop, float volume) {
            name = name.ToLower();
            lock (this._lock) {
                var audio = ServiceProvider.ResourceManagerRegistry.GetResourceManager(ResourceType.Audio);
                var resource = audio.GetResource(name);
                Sound? sound = null;
                if (resource is string path) {
                    sound = new Sound(new SoundBuffer(path)) {
                        Loop = false,
                        Volume = volume
                    };
                }
                if (resource is byte[] data) {
                    sound = new Sound(new SoundBuffer(data)) {
                        Loop = false,
                        Volume = volume
                    };
                }
                sound.Play();
                this._playingAudio.Add(new PlayingAudio(id, sound));
            }
        }

        public void Destroy() {
            this.StopAllAudio();
        }
    }
}
