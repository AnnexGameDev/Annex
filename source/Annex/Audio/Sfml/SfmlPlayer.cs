using Annex.Assets;
using Annex.Events;
using System.Collections.Generic;
using static Annex.Audio.Sfml.Errors;

namespace Annex.Audio.Sfml
{
    public sealed class SfmlPlayer : IAudioService
    {
        public const string GameEventID = "sfml-audio-player-gc";
        private readonly List<SfmlPlayingAudio> _playingAudio;
        private readonly object _lock = new object();

        public readonly IAssetManager AudioAssetManager;

        public SfmlPlayer(IAssetManager audioManager) {
            this._playingAudio = new List<SfmlPlayingAudio>();
            this.AudioAssetManager = audioManager;

            ServiceProvider.EventService.AddEvent(PriorityType.SOUNDS, () => {
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

        public void Destroy() {
            this.StopPlayingAudio();
        }

        public IPlayingAudio PlayAudio(string audioFilePath) {
            return this.PlayAudio(audioFilePath, new AudioContext());
        }

        public IPlayingAudio PlayAudio(string audioFilePath, AudioContext context) {
            lock (this._lock) {
                var args = new SfmlAudioInitializerArgs(audioFilePath, context.BufferMode);
                if (!this.AudioAssetManager.GetAsset(args, out var asset)) {
                    Debug.Error(ASSET_LOAD_FAILED.Format(audioFilePath));
                }
                // TODO: Wait for https://github.com/SFML/SFML/pull/1185 support in C#
                var playingAudio = new SfmlPlayingAudio(context.ID, asset);
                playingAudio.Volume = context.Volume;
                playingAudio.Loop = context.Loop;
                playingAudio.Play();
                this._playingAudio.Add(playingAudio);
                return playingAudio;
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

        public IEnumerable<IAssetManager> GetAssetManagers() {
            yield return AudioAssetManager;
        }
    }
}
