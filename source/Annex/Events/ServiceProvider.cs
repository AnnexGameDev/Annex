using Annex.Events;

namespace Annex
{
    public static partial class ServiceProvider
    {
        public static EventService EventService => Locate<EventService>() ?? Provide<EventService>();
    }
}
