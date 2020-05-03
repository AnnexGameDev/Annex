#nullable enable
using Annex.Logging;
using System;
using System.Collections.Generic;

namespace Annex
{
    public static partial class ServiceProvider
    {
        private static readonly Dictionary<Type, IService> _services;

        static ServiceProvider() {
            _services = new Dictionary<Type, IService>();
        }

        public static T Provide<T>() where T : class, IService, new() {
            return Provide<T, T>();
        }

        public static T Provide<T, K>() where T : class, IService where K : T, new() {
            var oldService = Locate<T>();
            if (oldService != null) {
                oldService.Destroy();
            }
            _services[typeof(T)] = new K();
            Log.WriteLineTrace_Module(typeof(ServiceProvider).Name, $"Created '{typeof(K).Name}' under '{typeof(T).Name}'");
            return (T)_services[typeof(T)];
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
