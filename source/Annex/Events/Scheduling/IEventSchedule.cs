namespace Annex.Events.Scheduling
{
    public interface IEventSchedule
    {
        bool HasNext { get; }
        IEvent Next { get; }
    }
}