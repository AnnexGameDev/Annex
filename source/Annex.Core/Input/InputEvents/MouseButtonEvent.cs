namespace Annex_Old.Core.Input.InputEvents
{
    public abstract class MouseButtonEvent : MouseEvent
    {
        public MouseButton Button;

        public MouseButtonEvent(MouseButton button) {
            this.Button = button;
        }
    }
}