using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Annex
{
    public static class Debug
    {
        [Conditional("DEBUG")]
        public static void Assert(bool condition, [CallerLineNumber] int line = 0, [CallerMemberName] string callingMethod = "unknown", [CallerFilePath] string filePath = "unknown") {
            if (!condition) {
                Debug.Log($"Assertion failed in {filePath} on line {line} in the function {callingMethod}.");
                throw new System.Exception("Assertion failed.");
            }
        }

        [Conditional("DEBUG")]
        public static void Log(string line) {
            Logging.Log.Singleton.WriteLine(line);
        }
    }
}
