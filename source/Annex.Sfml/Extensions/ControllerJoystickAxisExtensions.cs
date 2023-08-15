using Annex.Core.Input;
using SFML.Window;

namespace Annex.Sfml.Extensions
{
    public static class ControllerJoystickAxisExtensions
    {
        public static Joystick.Axis ToSfml(this ControllerJoystickAxis axis) {
            return (Joystick.Axis)axis;
        }
    }
}
