using Annex.Scenes;

namespace Annex.Graphics.Contexts
{
    public class MouseButtonPressedEvent
    {
        public MouseButton Button;
        public float WorldX;
        public float WorldY;
        public int MouseX;
        public int MouseY;
    }

    public class MouseButtonReleasedEvent
    {
        public MouseButton Button;
        public float WorldX;
        public float WorldY;
        public int MouseX;
        public int MouseY;
    }
}
