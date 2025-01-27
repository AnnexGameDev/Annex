using Annex.Core.Graphics.Windows;

namespace Annex.Core.Input.InputEvents;

public class MouseButtonPressedEvent : MouseButtonEvent
{
    public float WindowX { get; }
    public float WindowY { get; }

    public MouseButtonPressedEvent(IWindow window, MouseButton button, float x, float y) : base(window, button)
    {
        this.WindowX = x;
        this.WindowY = y;
    }
}