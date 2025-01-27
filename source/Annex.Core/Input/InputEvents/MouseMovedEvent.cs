using Annex.Core.Graphics.Windows;

namespace Annex.Core.Input.InputEvents;

public class MouseMovedEvent : MouseEvent
{
    public float WindowX { get; }
    public float WindowY { get; }

    public MouseMovedEvent(IWindow window, float x, float y) : base(window)
    {
        this.WindowX = x;
        this.WindowY = y;
    }
}