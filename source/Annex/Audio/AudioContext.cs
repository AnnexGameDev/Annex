#nullable enable

namespace Annex.Audio
{
    public class AudioContext
    {
        public bool Loop = false;
        private float _volume = 100;
        public string? ID = null;
        public BufferMode BufferMode = BufferMode.None;
        public float Volume {
            get => this._volume;
            set {
                Debug.Assert(value > 0);
                Debug.Assert(value <= 100);
                this._volume = value;
            }
        }
    }

    public enum BufferMode
    {
        None,
        Buffered
    }
}
