using Annex_Old.Core.Data;
using Annex_Old.Core.Graphics;
using Annex_Old.Core.Input.InputEvents;

namespace Annex_Old.Core.Scenes.Components
{
    public interface IUIElement : IDrawable
    {
        string ElementID { get; set; }
        IVector2<float> Size { get; }
        IVector2<float> Position { get; }
        bool Visible { get; set; }

        bool IsInBounds(float x, float y);

        void OnLostFocus();
        void OnGainedFocus();
        void OnMouseButtonPressed(MouseButtonPressedEvent mouseButtonPressedEvent);
        void OnMouseButtonReleased(MouseButtonReleasedEvent mouseButtonReleasedEvent);
        void OnMouseMoved(MouseMovedEvent mouseMovedEvent);
        void OnKeyboardKeyPressed(KeyboardKeyPressedEvent keyboardKeyPressedEvent);
        void OnKeyboardKeyReleased(KeyboardKeyReleasedEvent keyboardKeyReleasedEvent);
        void OnMouseScrollWheelMoved(MouseScrollWheelMovedEvent mouseScrollWheelMovedEvent);
        void OnMouseLeft(MouseMovedEvent mouseMovedEvent);
    }
}
