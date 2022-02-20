namespace Annex.Core.Input.InputEvents
{
    public class MouseButtonPressedEvent : MouseButtonEvent
    {
        public int WindowX { get; }
        public int WindowY { get; }

        public MouseButtonPressedEvent(MouseButton button, int x, int y) : base(button) {
            this.WindowX = x;
            this.WindowY = y;
        }
    }
}