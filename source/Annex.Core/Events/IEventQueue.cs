namespace Annex_Old.Core.Events
{
    public interface IEventQueue : IDisposable
    {
        void Step();

        void Remove(Guid itemId);
        void Add(params IEvent[] items);
    }
}
