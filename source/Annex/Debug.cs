using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Annex
{
    public static class Debug
    {
        [Conditional("DEBUG")]
        public static void Assert(bool condition, string reason, [CallerLineNumber] int line = 0, [CallerMemberName] string callingMethod = "unknown", [CallerFilePath] string filePath = "unknown") {
            if (!condition) {
                throw new AssertionFailedException(reason, line, callingMethod, filePath);
            }
        }

        [Conditional("DEBUG")]
        public static void ErrorIf(bool condition, string reason, [CallerLineNumber] int line = 0, [CallerMemberName] string callingMethod = "unknown", [CallerFilePath] string filePath = "unknown") {
            Assert(!condition, reason);
        }
    }
}
