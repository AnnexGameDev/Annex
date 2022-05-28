namespace Annex_Old.Core.Input.InputEvents
{
    public class MouseScrollWheelMovedEvent
    {
        public double Delta { get; }

        public MouseScrollWheelMovedEvent(double delta) {
            this.Delta = delta;
        }
    }
}