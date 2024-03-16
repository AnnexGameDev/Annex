namespace Annex.Core.Events.Core
{
    public interface ICoreEventService
    {
        void Add(long priority, IEvent coreEvent);
        void Remove(Guid eventId);
        Task RunAsync();
    }
}