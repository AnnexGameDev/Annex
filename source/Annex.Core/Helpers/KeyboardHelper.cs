using Annex_Old.Core.Input.Platforms;

namespace Annex_Old.Core.Helpers
{
    public class KeyboardHelper
    {
        private static IPlatformKeyboardService? _keyboardService;

        public KeyboardHelper(IPlatformKeyboardService platformKeyboardService) {
            if (_keyboardService !=null) {
                throw new InvalidOperationException("Static helper resource is already instanciated");
            }
            _keyboardService = platformKeyboardService;
        }

        public static bool IsShiftPressed() {
            return _keyboardService?.IsShiftPressed() ?? false;
        }

        internal static bool IsControlPressed() {
            return _keyboardService?.IsControlPressed() ?? false;
        }
    }
}
