namespace Annex.Core.Events
{
    public interface IEvent
    {
        Guid Id { get; }

        void TimeElapsed(long elapsedTime);
    }
}
