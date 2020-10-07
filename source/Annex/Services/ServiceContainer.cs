﻿using System;
using System.Collections.Generic;

namespace Annex.Services
{
    public partial class ServiceContainer : IDisposable // Instance implementation
    {
        private readonly Dictionary<Type, IService> _services;
        public IEnumerable<IService> RegisteredServices => _services.Values;

        public ServiceContainer() {
            _services = new Dictionary<Type, IService>();
        }

        public T Provide<T>() where T : class, IService, new() {
            return Provide<T, T>();
        }

        public T Provide<T>(T instance) where T : class, IService {
            Debug.ErrorIf(_services.ContainsKey(typeof(T)), $"A service of type {typeof(T)} already exists");
            _services[typeof(T)] = instance;
            ServiceProvider.Log.WriteLineTrace_Module(typeof(ServiceContainer).Name, $"Created '{instance.GetType().Name}' under '{typeof(T).Name}'");
            return (T)_services[typeof(T)];
        }

        public T Provide<T, K>() where T : class, IService where K : T, new() {
            return Provide<T>(new K());
        }

        public T Resolve<T>() where T : class, IService {
            if (!_services.ContainsKey(typeof(T))) {
                throw new AssertionFailedException($"Service of type {typeof(T)} does not exist");
            }
            return (T)_services[typeof(T)];
        }

        public void Remove<T>() where T : class, IService {
            Debug.Assert(_services.ContainsKey(typeof(T)), $"Tried to remove a service of type {typeof(T)} which doesn't exist");
            _services.Remove(typeof(T));
        }

        public bool Exists<T>() where T : class, IService {
            return _services.ContainsKey(typeof(T));
        }

        public void Dispose() {
            foreach (var service in this._services.Values) {
                service.Destroy();
            }
        }
    }
}