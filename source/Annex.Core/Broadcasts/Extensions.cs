using Annex.Core.Services;

namespace Annex.Core.Broadcasts
{
    public static partial class Extensions
    {
        private static RegistrationOptions _singletonOptions = new() { Singleton = true };

        public static void RegisterBroadcast<T>(this IContainer container) {
            container.Register<IBroadcast<T>, Broadcast<T>>(_singletonOptions);
        }
    }
}