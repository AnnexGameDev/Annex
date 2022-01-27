namespace Annex.Core.Events
{
    public interface IEventScheduler
    {
        IEventGroup GetEventGroup(string blockId);
        IEventGroup CreateEventGroup(string blockId, GroupExecutionMode mode);

        void Run();
    }
}
