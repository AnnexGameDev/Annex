using Annex.Scenes;

namespace Annex.Graphics.Events
{
    public class KeyboardEvent : HardwareEvent
    {
        public KeyboardKey Key;
        public bool ShiftDown;
    }

    public class KeyboardKeyPressedEvent : KeyboardEvent
    {

    }

    public class KeyboardKeyReleasedEvent : KeyboardEvent
    {

    }
}
