namespace Annex.Core.Times
{
    public interface ITimeService
    {
        long Now { get; }
        long ElapsedTimeSince(long time);
    }
}