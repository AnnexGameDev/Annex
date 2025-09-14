namespace Annex.Core.Time
{
    public interface ITimeService
    {
        long Now { get; }
        float NowF { get; }
        long ElapsedTimeSince(long time);
    }
}