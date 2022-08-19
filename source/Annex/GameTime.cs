using System.Diagnostics;

namespace Annex_Old
{
    public static class GameTime
    {
        private static readonly Stopwatch _sw;

        public static long Now => _sw.ElapsedMilliseconds;

        static GameTime() {
            _sw = new Stopwatch();
            _sw.Start();
        }

        public static long Since(long time) {
            return Now - time;
        }
    }
}
