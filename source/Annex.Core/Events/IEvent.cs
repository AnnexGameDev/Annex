namespace Annex_Old.Core.Events
{
    public interface IEvent
    {
        Guid Id { get; }

        void TimeElapsed(long elapsedTime);
    }
}
