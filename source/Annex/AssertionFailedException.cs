using System;
using static Annex.Paths;

namespace Annex
{
    public class AssertionFailedException : Exception
    {
        private static string FormatExceptionMessage(string reason, int line, string callingMethod, string filePath) {
            string relativeFilePath = filePath[SolutionFolder.Length..];
            return $"Failure in {relativeFilePath} on line {line} in the function {callingMethod}: {reason}";
        }

        public AssertionFailedException(string reason, int line, string callingMethod, string filePath)
            : base(FormatExceptionMessage(reason, line, callingMethod, filePath)) {
        }
    }
}
