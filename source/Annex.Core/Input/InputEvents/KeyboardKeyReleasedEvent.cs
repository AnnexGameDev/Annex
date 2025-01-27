using Annex.Core.Graphics.Windows;

namespace Annex.Core.Input.InputEvents;

public class KeyboardKeyReleasedEvent : KeyboardEvent
{
    public KeyboardKeyReleasedEvent(IWindow window, KeyboardKey key) : base(window, key)
    {
    }
}