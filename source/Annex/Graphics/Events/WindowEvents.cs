namespace Annex.Graphics.Events
{
    public class WindowResizedEvent
    {
        public uint Width { get; }
        public uint Height { get; }

        public WindowResizedEvent(uint width, uint height) {
            this.Width = width;
            this.Height = height;
        }
    }
}
