using Scaffold.Platform;

namespace Annex.Core.Platform
{
    public class Clipboard
    {
        private static object _lock = new();
        private static IClipboardService? _clipboardServiceInstance = null;

        public Clipboard(IClipboardService clipboardService) {
            _clipboardServiceInstance = clipboardService;
        }

        public static void SetString(string text) {
            lock (_lock) {
                _clipboardServiceInstance!.SetString(text);
            }
        }

        public static string? GetString() {
            lock (_lock) {
                return _clipboardServiceInstance?.GetString();
            }
        }
    }
}
