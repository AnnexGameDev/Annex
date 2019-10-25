using Annex.Scenes;

namespace Annex.Graphics.Contexts
{
    public class MouseButtonEvent
    {
        public MouseButton Button;
        public float WorldX;
        public float WorldY;
        public int MouseX;
        public int MouseY;
    }

    public class MouseButtonPressedEvent : MouseButtonEvent
    {
        public bool DoubleClick;
    }

    public class MouseButtonReleasedEvent : MouseButtonEvent
    {
        public int TimeSinceClick;
    }
}
