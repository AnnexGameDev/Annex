using Annex.Core.Graphics.Windows;
using Annex.Core.Input.InputEvents;

namespace Annex.Core.Scenes.Components
{
    public class Scene : IScene
    {
        public virtual void OnEnter(OnSceneEnterEventArgs onSceneEnterEventArgs) {
        }

        public virtual void OnLeave(OnSceneLeaveEventArgs onSceneLeaveEventArgs) {
        }

        public virtual void OnKeyboardKeyPressed(IWindow window, KeyboardKeyPressedEvent keyboardKeyPressedEvent) {
        }

        public virtual void OnKeyboardKeyReleased(IWindow window, KeyboardKeyReleasedEvent keyboardKeyReleasedEvent) {
        }

        public virtual void OnWindowClosed(IWindow window) {
        }
    }
}