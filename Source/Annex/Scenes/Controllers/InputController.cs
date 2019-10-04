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

        internal virtual bool HandleSceneFocusMouseDown(int x, int y) {
            return false;
        }
    }
}
