using Annex.Scenes;
using Annex.Scenes.Components;

namespace Annex.Graphics.Events
{
    public class MouseButtonEvent : HardwareEvent
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
        public long TimeSinceClick;
    }

    public class MouseMovedEvent
    {
        public float WorldX;
        public float WorldY;
        public int MouseX;
        public int MouseY;

        public (int relativeX, int relativeY) RelativeTo(UIElement element) {
            return (this.MouseX - (int)element.Position.X, this.MouseY - (int)element.Position.Y);
        }
    }
}
