using Annex.Core.Data;
using Annex.Core.Graphics.Windows;

namespace Annex.Core.Input
{
    public interface IInputService
    {
        // TODO: Tests
        bool IsKeyDown(KeyboardKey key);
        bool IsMouseButtonDown(MouseButton button);

        void HandleWindowClosed(IWindow window);
        void HandleKeyboardKeyPressed(IWindow window, KeyboardKey key);
        void HandleKeyboardKeyReleased(IWindow window, KeyboardKey key);

        void HandleMouseButtonPressed(IWindow window, MouseButton button, IVector2<float> position);
        void HandleMouseButtonReleased(IWindow window, MouseButton button, IVector2<float> position);
        void HandleMouseMoved(IWindow window, IVector2<float> position);
        void HandleMouseScrollWheelMoved(IWindow window, double delta);
    }
}