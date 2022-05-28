namespace Annex_Old.Core.Input.InputEvents
{
    public class MouseDoubleClickEvent : MouseButtonEvent
    {
        public int WindowX { get; }
        public int WindowY { get; }

        public MouseDoubleClickEvent(MouseButton button, int x, int y) : base(button) {
            this.WindowX = x;
            this.WindowY = y;
        }
    }
}