using Annex.Core.Services;

namespace Annex.Core.Broadcasts
{
    public static partial class Extensions
    {
        public static void RegisterBroadcast<T>(this IContainer container) {
            container.Register<IBroadcast<T>, Broadcast<T>>();
        }
    }
}