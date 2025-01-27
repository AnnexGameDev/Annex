using Annex.Core.Graphics.Windows;

namespace Annex.Core.Input.InputEvents;

public abstract class MouseEvent : WindowEvent
{
    public MouseEvent(IWindow window) : base(window)
    {
    }
}