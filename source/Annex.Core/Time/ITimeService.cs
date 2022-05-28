namespace Annex_Old.Core.Time
{
    public interface ITimeService
    {
        long Now { get; }
        long ElapsedTimeSince(long time);
    }
}