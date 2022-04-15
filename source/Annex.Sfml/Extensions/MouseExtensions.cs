using Annex.Core.Input;
using SFML.Window;

namespace Annex.Sfml.Extensions
{
    public static class MouseExtensions
    {
        public static MouseButton ToMouseButton(this Mouse.Button button) {
            return (MouseButton)button;
        }

        public static Mouse.Button ToSfml(this MouseButton button) {
            return (Mouse.Button)button;
        }
    }
}