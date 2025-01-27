using Annex.Core.Graphics.Windows;

namespace Annex.Core.Input.InputEvents;

public class MouseDoubleClickEvent : MouseButtonEvent
{
    public int WindowX { get; }
    public int WindowY { get; }

    public MouseDoubleClickEvent(IWindow window, MouseButton button, int x, int y) : base(window, button)
    {
        this.WindowX = x;
        this.WindowY = y;
    }
}