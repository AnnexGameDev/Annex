using Annex_Old.Events.Trackers;

namespace Annex_Old.Events
{
    public interface IEvent
    {
        string EventID { get; }
        EventArgs Probe(long timeDifference_ms);
        void MarkForRemoval();
        void AttachTracker(IEventTracker tracker);
    }
}
