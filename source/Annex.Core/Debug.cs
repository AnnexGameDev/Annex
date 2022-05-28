using System.Diagnostics;

namespace Annex_Old.Core
{
    public class Debug
    {
        [Conditional("DEBUG")]
        public static void Assert(bool condition, string reason) {
            System.Diagnostics.Debug.Assert(condition, reason);
        }
    }
}