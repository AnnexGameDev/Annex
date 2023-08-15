using System.Diagnostics;

namespace Annex.Core.Time
{
    internal class StopwatchTimeService : ITimeService
    {
        private readonly Stopwatch _sw = new();

        public long Now => this._sw.ElapsedMilliseconds;

        public StopwatchTimeService() {
            this._sw.Start();
        }

        public long ElapsedTimeSince(long time) {
            return this.Now - time;
        }
    }
}