namespace Annex_Old.Core.Events.Core
{
    public interface ICoreEventService
    {
        void Add(long priority, IEvent coreEvent);
        void Remove(Guid eventId);
        void Run();
    }
}