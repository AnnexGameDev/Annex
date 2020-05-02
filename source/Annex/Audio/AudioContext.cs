#nullable enable

namespace Annex.Audio
{
    public class AudioContext
    {
        public bool Loop { get; set; } = false;
        public string? ID { get; set; } = null;
        public BufferMode BufferMode { get; set; } = BufferMode.None;
        public float Volume { get; set; } = 100;
    }

    public enum BufferMode
    {
        None,
        Buffered
    }
}
