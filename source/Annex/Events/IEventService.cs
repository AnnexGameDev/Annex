using Annex_Old.Services;

namespace Annex_Old.Events
{
    public interface IEventService : IService
    {
        void AddEvent(PriorityType priority, IEvent e);
        void Run(ITerminationCondition terminationCondition);
        IEvent? GetEvent(string id);
    }
}
