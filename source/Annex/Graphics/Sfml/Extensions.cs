using Annex.Scenes;
using SFML.Window;

namespace Annex.Graphics.Sfml
{
    public static class Extensions
    {
        public static Keyboard.Key ToSFML(this KeyboardKey key) {
            return (Keyboard.Key)key;
        }

        public static Mouse.Button ToSFML(this MouseButton button) {
            return (Mouse.Button)button;
        }

        public static KeyboardKey ToNonSFML(this Keyboard.Key key) {
            return (KeyboardKey)key;
        }

        public static MouseButton ToNonSFML(this Mouse.Button button) {
            return (MouseButton)button;
        }

        public static JoystickAxis ToNonSFML(this Joystick.Axis axis) {
            return (JoystickAxis)axis;
        }
    }
}
