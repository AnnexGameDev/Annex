using System.Diagnostics;

namespace Annex
{
    public static class GameTime
    {
        private static readonly Stopwatch _sw;

        public static long Now => _sw.ElapsedMilliseconds;

        static GameTime() {
            _sw = new Stopwatch();
            _sw.Start();
        }
    }
}
