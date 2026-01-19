using Annex.Core.Assets;
using Annex.Core.Graphics.Windows;
using Annex.Core.Input.InputEvents;

namespace Annex.Core.Scenes.Elements;

public interface IScene : IUIElement, IAddableParentElement
{
    AssetRegistry Assets { get; }

    void OnLeave(OnSceneLeaveEventArgs onSceneLeaveEventArgs);
    void OnEnter(OnSceneEnterEventArgs onSceneEnterEventArgs);

    void OnWindowClosed(IWindow window);
    void OnKeyboardKeyPressed(IWindow window, KeyboardKeyPressedEvent keyboardKeyPressedEvent);
    void OnKeyboardKeyReleased(IWindow window, KeyboardKeyReleasedEvent keyboardKeyReleasedEvent);

    void OnMouseButtonPressed(IWindow window, MouseButtonPressedEvent mouseButtonPressedEvent);
    void OnMouseButtonReleased(IWindow window, MouseButtonReleasedEvent mouseButtonReleasedEvent);
    void OnMouseMoved(IWindow window, MouseMovedEvent mouseMovedEvent);
    void OnMouseScrollWheelMoved(IWindow window, MouseScrollWheelMovedEvent mouseScrollWheelMovedEvent);
    void OnWindowGainedFocus(IWindow window);
    void OnWindowLostFocus(IWindow window);
    void SetFocus(IUIElement? element);

    void AddChild(IUIElement child, bool focus = false);
}