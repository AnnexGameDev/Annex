using Annex_Old.Core.Events;
using Annex_Old.Core.Graphics.Windows;
using Annex_Old.Core.Input.InputEvents;

namespace Annex_Old.Core.Scenes.Components
{
    public interface IScene : IUIElement, IParentElement
    {
        IPriorityEventQueue Events { get; }

        void OnLeave(OnSceneLeaveEventArgs onSceneLeaveEventArgs);
        void OnEnter(OnSceneEnterEventArgs onSceneEnterEventArgs);

        void OnWindowClosed(IWindow window);
        void OnKeyboardKeyPressed(IWindow window, KeyboardKeyPressedEvent keyboardKeyPressedEvent);
        void OnKeyboardKeyReleased(IWindow window, KeyboardKeyReleasedEvent keyboardKeyReleasedEvent);

        void OnMouseButtonPressed(IWindow window, MouseButtonPressedEvent mouseButtonPressedEvent);
        void OnMouseButtonReleased(IWindow window, MouseButtonReleasedEvent mouseButtonReleasedEvent);
        void OnMouseMoved(IWindow window, MouseMovedEvent mouseMovedEvent);
        void OnMouseScrollWheelMoved(IWindow window, MouseScrollWheelMovedEvent mouseScrollWheelMovedEvent);
    }
}