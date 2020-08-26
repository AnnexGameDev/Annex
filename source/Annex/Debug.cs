using System.Diagnostics;
using System.Runtime.CompilerServices;
using static Annex.Paths;

namespace Annex
{
    public static class Debug
    {
        private static string FormatAndLog(string reason, int line, string callingMethod, string filePath) {
            string relativeFilePath = filePath[SolutionFolder.Length..];
            return $"Failure in {relativeFilePath} on line {line} in the function {callingMethod}: {reason}";
        }

        [Conditional("DEBUG")]
        public static void Assert(bool condition, string reason, [CallerLineNumber] int line = 0, [CallerMemberName] string callingMethod = "unknown", [CallerFilePath] string filePath = "unknown") {
            if (!condition) {
                throw new AssertionFailedException(FormatAndLog(reason, line, callingMethod, filePath));
            }
        }

        [Conditional("DEBUG")]
        public static void ErrorIf(bool condition, string reason, [CallerLineNumber] int line = 0, [CallerMemberName] string callingMethod = "unknown", [CallerFilePath] string filePath = "unknown") {
            Assert(!condition, reason);
        }
    }
}
