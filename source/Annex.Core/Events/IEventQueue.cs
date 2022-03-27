namespace Annex.Core.Events
{
    public interface IEventQueue
    {
        void Step();

        void Remove(Guid itemId);
        void Add(params IEvent[] items);
    }
}
