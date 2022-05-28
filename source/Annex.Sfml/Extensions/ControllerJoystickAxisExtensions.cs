using Annex_Old.Core.Input;
using SFML.Window;

namespace Annex_Old.Sfml.Extensions
{
    public static class ControllerJoystickAxisExtensions
    {
        public static Joystick.Axis ToSfml(this ControllerJoystickAxis axis) {
            return (Joystick.Axis)axis;
        }
    }
}
