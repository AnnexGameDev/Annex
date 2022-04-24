using Scaffold.Platform;

namespace Annex.Core.Helpers
{
    public class ClipboardHelper
    {
        private static object _lock = new();
        private static IClipboardService? _clipboardServiceInstance = null;

        public ClipboardHelper(IClipboardService clipboardService) {
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
