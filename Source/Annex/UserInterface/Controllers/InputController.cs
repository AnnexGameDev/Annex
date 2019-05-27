namespace Annex.UserInterface.Controllers
{
    public abstract class InputController
    {
        public virtual void HandleCloseButtonPressed() {

        }

        public virtual void HandleMouseButtonPressed(MouseButton button) {

        }

        public virtual void HandleMouseButtonReleased(MouseButton button) {

        }

        public virtual void HandleKeyboardKeyPressed(KeyboardKey key) {

        }
        
        public virtual void HandleKeyboardKeyReleased(KeyboardKey key) {

        }
    }
}
