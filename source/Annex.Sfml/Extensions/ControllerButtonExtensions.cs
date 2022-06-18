using Annex.Core.Input;

namespace Annex.Sfml.Extensions
{
    public static class ControllerButtonExtensions
    {
        public static uint ToSfml(this ControllerButton button) {
            return (uint)button;
        }
    }
}
