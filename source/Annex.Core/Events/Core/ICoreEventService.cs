namespace Annex.Core.Events.Core
{
    public interface ICoreEventService
    {
        void Add(CoreEventPriority priority, IEvent coreEvent);
        void Add(long priority, IEvent coreEvent);
        void Remove(Guid eventId);
        void Run();
    }
}