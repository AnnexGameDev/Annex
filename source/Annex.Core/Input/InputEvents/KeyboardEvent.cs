using Annex.Core.Graphics.Windows;

namespace Annex.Core.Input.InputEvents;

public abstract class KeyboardEvent : WindowEvent
{
    public KeyboardKey Key { get; }

    public KeyboardEvent(IWindow window, KeyboardKey key) : base(window)
    {
        this.Key = key;
    }
}