namespace Annex.Core.Events
{
    public interface IEventQueue : IDisposable
    {
        Task StepAsync();

        void Remove(Guid itemId);
        void Add(params IEvent[] items);
    }
}
