using Annex.Core.Graphics.Windows;

namespace Annex.Core.Input.InputEvents;

public class MouseScrollWheelMovedEvent : WindowEvent
{
    public double Delta { get; }

    public MouseScrollWheelMovedEvent(IWindow window, double delta) : base(window)
    {
        this.Delta = delta;
    }
}