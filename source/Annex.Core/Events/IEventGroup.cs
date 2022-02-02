namespace Annex.Core.Events
{
    public interface IEventGroup
    {
        int Priority { get; }
        string GroupId { get; }
        GroupExecutionMode ExecutionMode { get; }

        void Add(IEvent gameEvent);
        void Remove(string eventId);

        void Run(long elapsedTime);
    }
}
