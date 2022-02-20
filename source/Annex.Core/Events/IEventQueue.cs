namespace Annex.Core.Events
{
    public interface IEventQueue
    {
        void Run();
        void Stop();

        void Remove(Guid itemId);
        void Add(params IEvent[] items);
    }
}
