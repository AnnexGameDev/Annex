using Annex.Core.Graphics.Windows;

namespace Annex.Core.Input
{
    public interface IInputHandlerService
    {
        void HandleWindowClosed(IWindow window);
        void HandleKeyboardKeyPressed(IWindow window, KeyboardKey key);
        void HandleKeyboardKeyReleased(IWindow window, KeyboardKey key);
    }
}