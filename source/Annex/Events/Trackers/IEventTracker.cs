namespace Annex.Events.Trackers
{
    public interface IEventTracker
    {
        void NotifyProbe(IEvent gameEvent, long timeDiff, bool invoked);
    }
}
