using Annex.Logging;

namespace Annex
{
    public static partial class ServiceProvider
    {
        public static Log Log => Locate<Log>() ?? Provide<Log>(new Log());
    }
}
