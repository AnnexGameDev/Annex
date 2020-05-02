using SFML.Audio;
using System;

namespace Annex.Audio.Sfml
{
    internal class PlayingAudio : IPlayingAudio
    {
        internal readonly string Id;
        internal readonly object Audio;
        public string ID => this.Id;

        public bool IsPlaying => GetSoundStatus() == SoundStatus.Playing;
        public bool IsStopped => GetSoundStatus() == SoundStatus.Stopped;
        public float Volume { get => this.GetVolume(); set => this.SetVolume(value); }

        internal PlayingAudio(string id, Sound sound) {
            this.Id = id;
            this.Audio = sound;
        }

        internal PlayingAudio(string id, Music music) {
            this.Id = id;
            this.Audio = music;
        }

        public void Dispose() {
            if (this.Audio is Music music) {
                music.Dispose();
            }
            if (this.Audio is Sound sound) {
                sound.Dispose();
            }
        }

        internal void Play() {
            if (this.Audio is Music music) {
                music.Play();
            }
            if (this.Audio is Sound sound) {
                sound.Play();
            }
        }

        internal void Stop() {
            if (this.Audio is Music music) {
                music.Stop();
            }
            if (this.Audio is Sound sound) {
                sound.Stop();
            }
        }

        private SoundStatus GetSoundStatus() {
            if (this.Audio is Music music) {
                return music.Status;
            } else {
                return ((Sound)this.Audio).Status;
            }
        }

        private float GetVolume() {
            if (this.Audio is Music music) {
                return music.Volume;
            } else {
                return ((Sound)this.Audio).Volume;
            }
        }

        private void SetVolume(float volume) {
            Debug.Assert(volume >= 0 && volume <= 100, $"Invalid value for volume {volume}. Must be in the range [0,100]");
            if (this.Audio is Music music) {
                music.Volume = volume;
            } else {
                ((Sound)this.Audio).Volume = volume;
            }
        }
    }
}
