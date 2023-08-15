using Annex.Core.Data;
using Annex.Core.Graphics;
using Annex.Core.Input.InputEvents;

namespace Annex.Core.Scenes.Elements;

public interface IUIElement : IDrawable
{
    string ElementID { get; set; }
    IVector2<float> Size { get; }
    IVector2<float> Position { get; }
    bool Visible { get; set; }

    bool IsInBounds(float x, float y);

    void OnLostFocus();
    event EventHandler? OnElementLostFocus;

    void OnGainedFocus();
    event EventHandler? OnElementGainedFocus;

    void OnMouseButtonPressed(MouseButtonPressedEvent mouseButtonPressedEvent);
    event EventHandler<MouseButtonPressedEvent>? OnElementMouseButtonPressed;

    void OnMouseButtonReleased(MouseButtonReleasedEvent mouseButtonReleasedEvent);
    event EventHandler<MouseButtonReleasedEvent>? OnElementMouseButtonReleased;

    void OnMouseMoved(MouseMovedEvent mouseMovedEvent);
    event EventHandler<MouseMovedEvent>? OnElementMouseMoved;

    void OnKeyboardKeyPressed(KeyboardKeyPressedEvent keyboardKeyPressedEvent);
    event EventHandler<KeyboardKeyPressedEvent>? OnElementKeyboardKeyPressed;

    void OnKeyboardKeyReleased(KeyboardKeyReleasedEvent keyboardKeyReleasedEvent);
    event EventHandler<KeyboardKeyReleasedEvent>? OnElementKeyboardKeyReleased;

    void OnMouseScrollWheelMoved(MouseScrollWheelMovedEvent mouseScrollWheelMovedEvent);
    event EventHandler<MouseScrollWheelMovedEvent>? OnElementMouseScrollWheelMoved;

    void OnMouseLeft(MouseMovedEvent mouseMovedEvent);
    event EventHandler<MouseMovedEvent>? OnElementMouseLeft;
}
