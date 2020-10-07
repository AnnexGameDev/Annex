using Annex.Services;

namespace Annex.Events
{
    public interface IEventService : IService
    {
        void AddEvent(PriorityType priority, IEvent e);
        void Run(ITerminationCondition terminationCondition);
        IEvent? GetEvent(string id);
    }
}
