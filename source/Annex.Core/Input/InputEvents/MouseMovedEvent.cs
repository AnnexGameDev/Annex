namespace Annex_Old.Core.Input.InputEvents
{
    public class MouseMovedEvent : MouseEvent
    {
        public int WindowX { get; }
        public int WindowY { get; }

        public MouseMovedEvent(int x, int y) {
            this.WindowX = x;
            this.WindowY = y;
        }
    }
}