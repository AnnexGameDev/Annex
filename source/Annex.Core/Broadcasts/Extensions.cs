using Scaffold.DependencyInjection;

namespace Annex.Core.Broadcasts
{
    public static partial class Extensions
    {
        public static void RegisterBroadcast<T>(this IContainer container) {
            container.RegisterSingleton<IBroadcast<T>, Broadcast<T>>();
        }
    }
}