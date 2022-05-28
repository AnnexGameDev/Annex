using Annex_Old.Core.Graphics.Windows;

namespace Annex_Old.Core.Input
{
    public interface IInputService
    {
        // TODO: Tests
        bool IsKeyDown(KeyboardKey key);
        bool IsMouseButtonDown(MouseButton button);

        void HandleWindowClosed(IWindow window);
        void HandleKeyboardKeyPressed(IWindow window, KeyboardKey key);
        void HandleKeyboardKeyReleased(IWindow window, KeyboardKey key);

        void HandleMouseButtonPressed(IWindow window, MouseButton button, int windowX, int windowY);
        void HandleMouseButtonReleased(IWindow window, MouseButton button, int windowX, int windowY);
        void HandleMouseMoved(IWindow window, int windowX, int windowY);
        void HandleMouseScrollWheelMoved(IWindow window, double delta);
    }
}