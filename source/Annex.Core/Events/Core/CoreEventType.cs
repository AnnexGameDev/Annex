namespace Annex.Core.Events.Core
{
    public enum CoreEventPriority : long
    {
        Networking = 0,
        UserInput = 1000,
        GameLogic = 2000,
        Graphics = 3000,
        Audio = 4000,
    }

    public static class Extensions
    {
        public static void Add(this IPriorityEventQueue priorityEventQueue, CoreEventPriority priority, IEvent @event) {
            priorityEventQueue.Add((long)priority, @event);
        }

        public static void Add(this ICoreEventService coreEventService, CoreEventPriority priority, IEvent @event) {
            coreEventService.Add((long)priority, @event);
        }
    }
}