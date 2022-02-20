namespace Annex.Core.Events.Core
{
    public interface ICoreEventService
    {
        void Add(CoreEventType type, IEvent coreEvent);
        void Run();
    }
}