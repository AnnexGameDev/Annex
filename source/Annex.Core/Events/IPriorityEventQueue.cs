namespace Annex.Core.Events
{
    public interface IPriorityEventQueue
    {
        IEnumerable<long> Priorities { get; }
        void Add(long priority, IEvent @event);
        void Remove(Guid eventId);
        void StepPriority(long priority);
    }
}
