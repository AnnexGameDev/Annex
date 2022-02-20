using Annex.Core.Graphics.Windows;
using Annex.Core.Input.InputEvents;
using Annex.Core.Scenes.Components;

namespace Annex.Core.Scenes
{
    public interface IScene
    {
        void OnLeave(OnSceneLeaveEventArgs onSceneLeaveEventArgs);
        void OnEnter(OnSceneEnterEventArgs onSceneEnterEventArgs);

        void OnWindowClosed(IWindow window);
        void OnKeyboardKeyPressed(IWindow window, KeyboardKeyPressedEvent keyboardKeyPressedEvent);
        void OnKeyboardKeyReleased(IWindow window, KeyboardKeyReleasedEvent keyboardKeyReleasedEvent);
    }
}