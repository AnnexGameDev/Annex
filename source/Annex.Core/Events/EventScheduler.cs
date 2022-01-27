namespace Annex.Core.Events
{
    internal class EventScheduler : IEventScheduler
    {
        // TODO: Implement these.
        // Also, ordering of groups
        // TimeService

        public IEventGroup CreateEventGroup(string blockId, GroupExecutionMode mode) {
        }

        public IEventGroup GetEventGroup(string blockId) {
        }

        public void Run() {
        }
    }
}