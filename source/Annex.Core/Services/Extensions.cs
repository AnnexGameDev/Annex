namespace Annex.Core.Services
{
    public static class Extensions
    {
        private static RegistrationOptions _singletonOptions = new() { Singleton = true };

        public static void RegisterSingleton<T, K>(this IContainer container) where K : T {
            container.Register<T, K>(_singletonOptions);
        }

        public static void RegisterMessage<T>(this IContainer container) {
            container.Register<IMessage<T>, Message<T>>();
        }
    }
}