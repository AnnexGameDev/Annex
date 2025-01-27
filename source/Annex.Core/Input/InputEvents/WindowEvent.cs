using Annex.Core.Graphics.Windows;

namespace Annex.Core.Input.InputEvents;

public abstract class WindowEvent
{
    public bool Handled { get; set; } = false;
    public IWindow Window { get; }

    public WindowEvent(IWindow window)
    {
        Window = window;
    }
}