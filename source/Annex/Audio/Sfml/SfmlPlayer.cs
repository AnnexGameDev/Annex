using Annex.Assets.Converters;
using Annex.Audio.Sfml.Events;
using Annex.Events;
using Annex.Services;
using SFML.Audio;
using System;
using System.Collections.Generic;
using static Annex.Audio.Sfml.Errors;

namespace Annex.Audio.Sfml
{
    public sealed class SfmlPlayer : IAudioService
    {
        private readonly List<SfmlPlayingAudio> _playingAudio;
        private readonly object _lock = new object();

        private readonly IAssetConverter _converter = new ByteArrayConverter();

        public SfmlPlayer() {
            this._playingAudio = new List<SfmlPlayingAudio>();
            ServiceProvider.EventService.AddEvent(PriorityType.SOUNDS, new SoundGCEvent(this._lock, this._playingAudio));
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
                var args = new AssetConverterArgs(audioFilePath, this._converter);
                if (!ServiceProvider.AudioManager.GetAsset(args, out var asset)) {
                    Debug.Error(ASSET_LOAD_FAILED.Format(audioFilePath));
                }

                object target;
                if (context.BufferMode == BufferMode.Buffered) {
                    target = new Music((byte[])asset.GetTarget());
                } else {
                    target = new Sound(new SoundBuffer((byte[])asset.GetTarget()));
                }

                // TODO: Wait for https://github.com/SFML/SFML/pull/1185 support in C#
                var playingAudio = new SfmlPlayingAudio(context.ID, target);
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
    }
}
