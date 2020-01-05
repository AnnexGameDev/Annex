#nullable enable
using System;
using System.Collections.Generic;

namespace Annex
{
    public static partial class ServiceProvider
    {
        private static Dictionary<Type, IService> _services;

        static ServiceProvider() {
            _services = new Dictionary<Type, IService>();
        }

        public static T Provide<T>(IService service) where T : class, IService {
            var oldService = Locate<T>();
            if (oldService != null) {
                oldService.Destroy();
            }
            _services[typeof(T)] = service;
            return (T)service;
        }

        public static T? Locate<T>() where T : class, IService {
            if (!_services.ContainsKey(typeof(T))) {
                return null;
            }
            return _services[typeof(T)] as T;
        }
    }

    public interface IService
    {
        void Destroy();
    }
}
