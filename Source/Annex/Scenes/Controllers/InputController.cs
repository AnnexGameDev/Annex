using Annex.Graphics.Contexts;

namespace Annex.Scenes.Controllers
{
    public abstract class InputController
    {
        public virtual void HandleCloseButtonPressed() {

        }

        public virtual void HandleMouseButtonPressed(MouseButtonPressedEvent e) {

        }

        public virtual void HandleMouseButtonReleased(MouseButtonReleasedEvent e) {

        }

        public virtual void HandleKeyboardKeyPressed(KeyboardKey key) {

        }
        
        public virtual void HandleKeyboardKeyReleased(KeyboardKey key) {

        }

        public virtual void HandleJoystickMoved(JoystickMovedEvent e) {

        }

        public virtual void HandleJoystickButtonPressed(JoystickButtonPressedEvent e) {

        }

        public virtual void HandleJoystickButtonReleased(JoystickButtonReleasedEvent e) {

        }

        public virtual void HandleJoystickDisconnected(JoystickDisconnectedEvent e) {

        }

        public virtual void HandleJoystickConnected(JoystickConnectedEvent e) {

        }

        internal virtual bool HandleSceneFocusMouseDown(int x, int y) {
            return false;
        }
    }
}
