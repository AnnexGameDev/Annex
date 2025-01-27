using Annex.Core.Graphics.Windows;

namespace Annex.Core.Input.InputEvents;

public class MouseButtonReleasedEvent : MouseButtonEvent
{
    public float WindowX { get; }
    public float WindowY { get; }

    public MouseButtonReleasedEvent(IWindow window, MouseButton button, float x, float y) : base(window, button)
    {
        this.WindowX = x;
        this.WindowY = y;
    }
}