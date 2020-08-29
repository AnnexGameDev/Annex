namespace Annex.Events.Scheduling
{
    public interface ISchedulingEngine
    {
        void Schedule(IEvent e);
        IEventSchedule GetEventSchedule();
    }
}