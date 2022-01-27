namespace Annex.Core.Events
{
    public interface IEvent
    {
        string EventId { get; }
        void Probe(long timeDifference_ms);
    }
}
