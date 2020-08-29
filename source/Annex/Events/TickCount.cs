using System.Diagnostics;

namespace Annex.Events
{
    // Environment.TickCount is based on GetTickCount() WinAPI function. It's in milliseconds But the actual precision of it is about 15.6 ms. 
    // So you can't measure shorter time intervals (or you'll get 0)                                                                  
    // [ ^ this gave me a lot of headache. Current workaround to get more precise time diffs is using stopwatch ]
    //                                                                                                                          
    // Source: https://stackoverflow.com/questions/243351/environment-tickcount-vs-datetime-now/6308701#6308701
    public static class TickCount
    {
        private static readonly Stopwatch _sw;
        public static long Current => TickCount._sw.ElapsedMilliseconds;

        static TickCount() {
            TickCount._sw = new Stopwatch();
            TickCount._sw.Start();
        }
    }
}
