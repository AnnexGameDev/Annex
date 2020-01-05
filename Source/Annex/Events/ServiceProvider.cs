using Annex.Events;

namespace Annex
{
    public static partial class ServiceProvider
    {
        public static EventManager EventManager => Locate<EventManager>() ?? Provide<EventManager>(new EventManager());
    }
}
