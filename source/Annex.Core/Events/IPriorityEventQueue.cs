namespace Annex_Old.Core.Events
{
    public interface IPriorityEventQueue : IDisposable
    {
        IEnumerable<long> Priorities { get; }
        void Add(long priority, IEvent @event);
        void Remove(Guid eventId);
        void StepPriority(long priority);
    }
}
