namespace Annex.Core.Input.InputEvents
{
    public class MouseButtonPressedEvent : MouseButtonEvent
    {
        public float WindowX { get; }
        public float WindowY { get; }

        public MouseButtonPressedEvent(MouseButton button, float x, float y) : base(button) {
            this.WindowX = x;
            this.WindowY = y;
        }
    }
}