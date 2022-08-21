namespace Annex.Core.Input.InputEvents
{
    public class MouseMovedEvent : MouseEvent
    {
        public float WindowX { get; }
        public float WindowY { get; }

        public MouseMovedEvent(float x, float y) {
            this.WindowX = x;
            this.WindowY = y;
        }
    }
}