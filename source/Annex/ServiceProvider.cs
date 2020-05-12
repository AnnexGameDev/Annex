#nullable enable
using Annex.Assets;
using Annex.Logging;
using System;
using System.Collections.Generic;

namespace Annex
{
    public static partial class ServiceProvider
    {
        private static readonly Dictionary<Type, IService> _services;
        public static IEnumerable<IService> RegisteredServices => _services.Values;

        static ServiceProvider() {
            _services = new Dictionary<Type, IService>();
        }

        public static T Provide<T>() where T : class, IService, new() {
            return Provide<T, T>();
        }

        public static T Provide<T>(T instance) where T : class, IService {
            Locate<T>()?.Destroy();
            _services[typeof(T)] = instance;
            Log.WriteLineTrace_Module(typeof(ServiceProvider).Name, $"Created '{instance.GetType().Name}' under '{typeof(T).Name}'");
            return (T)_services[typeof(T)];
        }

        public static T Provide<T, K>() where T : class, IService where K : T, new() {
            return Provide<T>(new K());
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
        IEnumerable<IAssetManager> GetAssetManagers();
    }
}
