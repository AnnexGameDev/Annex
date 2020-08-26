using System.Diagnostics;

namespace Annex
{
    public static class Debug
    {
        [Conditional("DEBUG")]
        public static void Assert(bool condition, string message) {
            if (!condition) {
                throw new AssertionFailedException(message);
            }
        }

        [Conditional("DEBUG")]
        public static void ErrorIf(bool condition, string message) {
            Assert(!condition, message);
        }
    }
}
