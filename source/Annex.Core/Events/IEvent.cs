namespace Annex.Core.Events
{
    public interface IEvent
    {
        Guid Id { get; }

        Task TimeElapsedAsync(long elapsedTime);
    }
}
