using SFML.Audio;
using System;

namespace Annex.Audio.Players.Sfml
{
    internal class PlayingAudio : IDisposable
    {
        internal readonly string Id;
        internal readonly object Audio;

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

        internal bool IsStopped() {
            if (this.Audio is Music music) {
                return music.Status == SoundStatus.Stopped;
            }
            else {
                return ((Sound)this.Audio).Status == SoundStatus.Stopped;
            }
        }
    }
}
