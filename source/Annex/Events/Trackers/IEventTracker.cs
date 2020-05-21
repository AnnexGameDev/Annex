namespace Annex.Events.Trackers
{
    public interface IEventTracker
    {
        void NotifyProbe(GameEvent gameEvent, long timeDiff, bool invoked);
    }
}
