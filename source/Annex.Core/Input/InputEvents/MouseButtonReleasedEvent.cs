namespace Annex.Core.Input.InputEvents
{
    public class MouseButtonReleasedEvent : MouseButtonEvent
    {
        public float WindowX { get; }
        public float WindowY { get; }

        public MouseButtonReleasedEvent(MouseButton button, float x, float y) : base(button) {
            this.WindowX = x;
            this.WindowY = y;
        }
    }
}