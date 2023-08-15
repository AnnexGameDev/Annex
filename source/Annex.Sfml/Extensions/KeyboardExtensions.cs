using Annex.Core.Input;
using SFML.Window;

namespace Annex.Sfml.Extensions
{
    public static class KeyboardExtensions
    {
        public static KeyboardKey ToKeyboardKey(this Keyboard.Key key) {
            return (KeyboardKey)key;
        }

        public static Keyboard.Key ToSfmlKeyboardKey(this KeyboardKey key) {
            return (Keyboard.Key)key;
        }
    }
}