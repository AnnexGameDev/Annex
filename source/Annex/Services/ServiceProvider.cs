using System;

namespace Annex.Services
{
    public class ServiceProvider : IDisposable
    {
        private static ServiceProvider? _serviceProviderSingleton;
        private readonly IServiceContainer _container;

        public static ServiceProvider CreateSingleton(ServiceProvider provider) {
            if (_serviceProviderSingleton == null) {
                ServiceProvider._serviceProviderSingleton = provider;
            }
            return ServiceProvider._serviceProviderSingleton!;
        }

        public static void DestroySingleton() {
            ServiceProvider._serviceProviderSingleton?.Dispose();
        }

        public static ServiceProvider GetSingleton() {
            return ServiceProvider._serviceProviderSingleton!;
        }

        public ServiceProvider(IServiceContainer container) {
            this._container = container;
        }

        ~ServiceProvider() {
            this.Dispose();
        }

        public static T? Provide<T>(T instance) where T : class, IService {
            var container = ServiceProvider.GetSingleton()?._container;
            container?.SetService(typeof(T), instance);
            return container?.GetService(typeof(T)) as T;
        }

        public static T? Resolve<T>() where T : class, IService {
            return ServiceProvider.GetSingleton()?._container?.GetService(typeof(T)) as T;
        }

        public void Dispose() {
            this._container?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
