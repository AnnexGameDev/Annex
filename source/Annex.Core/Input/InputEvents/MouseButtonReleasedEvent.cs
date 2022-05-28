namespace Annex_Old.Core.Input.InputEvents
{
    public class MouseButtonReleasedEvent : MouseButtonEvent
    {
        public int WindowX { get; }
        public int WindowY { get; }

        public MouseButtonReleasedEvent(MouseButton button, int x, int y) : base(button) {
            this.WindowX = x;
            this.WindowY = y;
        }
    }
}