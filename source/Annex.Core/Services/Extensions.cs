namespace Annex.Core.Services
{
    public static partial class Extensions
    {
        private static RegistrationOptions _singletonOptions = new() { Singleton = true };

        public static void RegisterSingleton<T, K>(this IContainer container) where K : T {
            container.Register<T, K>(_singletonOptions);
        }
    }
}