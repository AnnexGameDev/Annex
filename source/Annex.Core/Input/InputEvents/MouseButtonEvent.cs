using Annex.Core.Graphics.Windows;

namespace Annex.Core.Input.InputEvents;

public abstract class MouseButtonEvent : MouseEvent
{
    public MouseButton Button;

    public MouseButtonEvent(IWindow window, MouseButton button) : base(window)
    {
        this.Button = button;
    }
}