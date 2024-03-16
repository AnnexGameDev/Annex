namespace Annex.Core.Events
{
    public interface IPriorityEventQueue : IDisposable
    {
        IEnumerable<long> Priorities { get; }
        void Add(long priority, IEvent @event);
        void Add(long priority, long interval, long delay, Action timeElapsedDelegate);
        void Remove(Guid eventId);
        Task StepPriorityAsync(long priority);
    }
}
