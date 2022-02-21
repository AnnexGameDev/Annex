using Annex.Core.Graphics;
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

        public virtual void OnMouseButtonPressed(IWindow window, MouseButtonPressedEvent mouseButtonPressedEvent) {
        }

        public virtual void OnMouseButtonReleased(IWindow window, MouseButtonReleasedEvent mouseButtonReleasedEvent) {
        }

        public virtual void OnMouseMoved(IWindow window, MouseMovedEvent mouseMovedEvent) {
        }

        public virtual void OnMouseScrollWheelMoved(IWindow window, MouseScrollWheelMovedEvent mouseScrollWheelMovedEvent) {
        }

        public virtual void Draw(ICanvas canvas) {
        }
    }
}