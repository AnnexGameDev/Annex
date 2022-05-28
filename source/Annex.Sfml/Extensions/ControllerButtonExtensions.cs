using Annex_Old.Core.Input;

namespace Annex_Old.Sfml.Extensions
{
    public static class ControllerButtonExtensions
    {
        public static uint ToSfml(this ControllerButton button) {
            return (uint)button;
        }
    }
}
