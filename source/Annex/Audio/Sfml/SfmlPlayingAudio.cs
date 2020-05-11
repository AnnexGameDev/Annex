#nullable enable
using SFML.Audio;
using static Annex.Strings.Errors.Audio.Sfml;

namespace Annex.Audio.Sfml
{
    internal class SfmlPlayingAudio : IPlayingAudio
    {
        internal readonly string Id;
        internal readonly dynamic Audio;

        private bool _isDisposed;
        public string ID => this.Id;
        public bool IsPlaying => GetSoundStatus() == SoundStatus.Playing;
        public bool IsStopped => GetSoundStatus() == SoundStatus.Stopped;
        public float Volume { get => this.GetVolume(); set => this.SetVolume(value); }
        public bool Loop { get => this.GetLoop(); set => this.SetLoop(value); }

        internal SfmlPlayingAudio(string id, object resource) {
            Debug.Assert(resource is Music || resource is Sound, INVALID_RESOURCE.Format(resource.GetType().Name));
            this.Id = id;
            this.Audio = resource;
        }

        public void Dispose() {
            Debug.ErrorIf(this._isDisposed, PERFORMED_OPERATION_WHILE_DISPOSED.Format(nameof(Dispose)));
            lock (this) {
                this.Audio.Dispose();
                this._isDisposed = true;
            }
        }

        internal void Play() {
            if (this._isDisposed) {
                Debug.Error(PERFORMED_OPERATION_WHILE_DISPOSED.Format(nameof(Play)));
            }
            lock (this) {
                this.Audio.Play();
            }
        }

        internal void Stop() {
            if (this._isDisposed) {
                Debug.Error(PERFORMED_OPERATION_WHILE_DISPOSED.Format(nameof(Stop)));
            }
            lock (this) {
                this.Audio.Stop();
            }
        }

        private SoundStatus GetSoundStatus() {
            if (this._isDisposed) {
                ServiceProvider.Log.WriteLineWarning(PERFORMED_OPERATION_WHILE_DISPOSED.Format(nameof(GetSoundStatus)));
                return SoundStatus.Stopped;
            }
            lock (this) {
                return this.Audio.Status;
            }
        }

        private float GetVolume() {
            if (this._isDisposed) {
                ServiceProvider.Log.WriteLineWarning(PERFORMED_OPERATION_WHILE_DISPOSED.Format(nameof(GetVolume)));
                return 0;
            }
            lock (this) {
                return this.Audio.Volume;
            }
        }

        private void SetVolume(float volume) {
            Debug.ErrorIf(volume < 0 || volume > 100, INVALID_VOLUME_VALUE.Format(volume));
            if (this._isDisposed) {
                ServiceProvider.Log.WriteLineWarning(PERFORMED_OPERATION_WHILE_DISPOSED.Format(nameof(SetVolume)));
                return;
            }
            lock (this) {
                this.Audio.Volume = volume;
            }
        }

        public bool GetLoop() {
            if (this._isDisposed) {
                ServiceProvider.Log.WriteLineWarning(PERFORMED_OPERATION_WHILE_DISPOSED.Format(nameof(GetLoop)));
                return false;
            }
            lock (this) {
                return this.Audio.Loop;
            }
        }

        public void SetLoop(bool loop) {
            if (this._isDisposed) {
                ServiceProvider.Log.WriteLineWarning(PERFORMED_OPERATION_WHILE_DISPOSED.Format(nameof(SetLoop)));
                return;
            }
            lock (this) {
                this.Audio.Loop = loop;
            }
        }
    }
}
