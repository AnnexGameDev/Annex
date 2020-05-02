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
    public sealed class SfmlPlayer : IAudioPlayer
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

            // Create the resource manager for audio if it does not already exist.
            var resources = ServiceProvider.ResourceManagerRegistry;
            var audio = resources.GetOrCreate<FSResourceManager>(ResourceType.Audio);
            audio.SetResourcePath(this.AudioPath);
            audio.SetResourceLoader(this.AudioLoader_Bytes);
            audio.SetResourceLoader(this.AudioLoader_String);
            audio.SetResourceValidator(this.AudioValidator);

            ServiceProvider.EventManager.AddEvent(PriorityType.SOUNDS, () => {
                lock (this._lock) {
                    for (int i = 0; i < this._playingAudio.Count; i++) {
                        var audio = this._playingAudio[i];
                        if (audio.IsStopped) {
                            audio.Stop();
                            audio.Dispose();
                            this._playingAudio.RemoveAt(i--);
                        }
                    }
                    return ControlEvent.NONE;
                }
            }, 5000, eventID: GameEventID);
        }

        public void StopPlayingAudio(string? id = null) {
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

        private IPlayingAudio PlayBufferedAudio(string name, string id, bool loop, float volume) {
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
                var playingAudio = new PlayingAudio(id, music);
                music.Play();
                this._playingAudio.Add(playingAudio);
                return playingAudio;
            }
        }

        private IPlayingAudio PlayUnbufferedAudio(string name, string id, bool loop, float volume) {
            name = name.ToLower();
            lock (this._lock) {
                var audio = ServiceProvider.ResourceManagerRegistry.GetResourceManager(ResourceType.Audio);
                var resource = audio.GetResource(name);
                Sound? sound = null;
                if (resource is string path) {
                    sound = new Sound(new SoundBuffer(path)) {
                        Loop = loop,
                        Volume = volume
                    };
                }
                if (resource is byte[] data) {
                    sound = new Sound(new SoundBuffer(data)) {
                        Loop = loop,
                        Volume = volume
                    };
                }
                var playingAudio = new PlayingAudio(id, sound);
                sound.Play();
                this._playingAudio.Add(playingAudio);
                return playingAudio;
            }
        }

        public void Destroy() {
            this.StopPlayingAudio();
        }

        public IPlayingAudio PlayAudio(string audioFilePath) {
            return this.PlayAudio(audioFilePath, new AudioContext());
        }

        public IPlayingAudio PlayAudio(string audioFilePath, AudioContext context) {
            switch (context.BufferMode) {
                case BufferMode.Buffered:
                    return PlayBufferedAudio(audioFilePath, context.ID ?? string.Empty, context.Loop, context.Volume);
                case BufferMode.None:
                    return PlayUnbufferedAudio(audioFilePath, context.ID ?? string.Empty, context.Loop, context.Volume);
                default:
                    Debug.Assert(false, $"BufferMode: {context.BufferMode} is unsupported");
#pragma warning disable CS8603 // Possible null reference return.
                    return null;
#pragma warning restore CS8603 // Possible null reference return.
            }
        }

        public IEnumerable<IPlayingAudio> GetPlayingAudio(string? id = null) {
            lock (this._lock) {
                for (int i = 0; i < this._playingAudio.Count; i++) {
                    var playingAudio = this._playingAudio[i];
                    if (id != null && playingAudio.ID != id) {
                        continue;
                    }
                    yield return playingAudio;
                }
            }
        }
    }
}
