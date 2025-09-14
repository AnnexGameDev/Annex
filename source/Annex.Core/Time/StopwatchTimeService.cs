using System.Diagnostics;

namespace Annex.Core.Time;

internal class StopwatchTimeService : ITimeService
{
    private readonly Stopwatch _sw = new();

    public long Now => _sw.ElapsedMilliseconds;
    public float NowF => Now / 1000.0f;

    public StopwatchTimeService()
    {
        _sw.Start();
    }

    public long ElapsedTimeSince(long time)
    {
        return Now - time;
    }
}