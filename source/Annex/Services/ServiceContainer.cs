using System;
using System.Collections.Generic;

namespace Annex.Services
{
    public class ServiceContainer : IServiceContainer
    {
        private readonly Dictionary<Type, object> _services;

        public ServiceContainer() {
            this._services = new Dictionary<Type, object>();
        }

        ~ServiceContainer() {
            this.Dispose();
        }

        public void Dispose() {
            foreach (var entry in _services) {
                (entry.Value as IService)?.Dispose();
            }
            this._services.Clear();
            GC.SuppressFinalize(this);
        }

        public object? GetService(Type serviceType) {
            if (this._services.TryGetValue(serviceType, out object? service)) {
                return service;
            }
            return null;
        }

        public void RemoveService(Type serviceType) {
            Debug.Assert(this._services.ContainsKey(serviceType), $"The service {serviceType} does not exist in the container");
            this._services.Remove(serviceType);
        }

        public void SetService(Type serviceType, object service) {
            this._services[serviceType] = service;
        }
    }
}