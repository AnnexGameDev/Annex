using Annex.Core.Data;
using Annex.Core.Graphics.Windows;

namespace Annex.Core.Input;

public interface IInputHandler
{
    // TODO: Tests
    bool IsKeyDown(KeyboardKey key);
    bool IsMouseButtonDown(MouseButton button);

    void HandleWindowClosed(IWindow window);
    void HandleKeyboardKeyPressed(IWindow window, KeyboardKey key);
    void HandleKeyboardKeyReleased(IWindow window, KeyboardKey key);

    void HandleMouseButtonPressed(IWindow window, MouseButton button, Position position);
    void HandleMouseButtonReleased(IWindow window, MouseButton button, Position position);
    void HandleMouseMoved(IWindow window, Position position);
    void HandleMouseScrollWheelMoved(IWindow window, double delta);

    void HandleWindowGainedFocus(IWindow window);
    void HandleWindowLostFocus(IWindow window);
}