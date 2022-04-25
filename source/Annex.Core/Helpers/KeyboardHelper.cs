using Annex.Core.Input.Platforms;

namespace Annex.Core.Helpers
{
    public class KeyboardHelper
    {
        private static IPlatformKeyboardService? _keyboardService;

        public KeyboardHelper(IPlatformKeyboardService platformKeyboardService) {
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
