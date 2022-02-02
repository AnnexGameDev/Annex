using Annex.Core.Logging;
using System.Collections;

namespace Annex.Core.Services
{
    internal class Container : IContainer
    {
        private class SingletonEntry
        {
            public Type Type { get; set; }
            public object? Instance { get; set; }
            public bool IsInstanciated => this.Instance != null;

            public SingletonEntry(Type type) {
                this.Type = type;
                this.Instance = null;
            }
        }

        private readonly Dictionary<Type, object> _serviceData = new();

        public Container() {
            // Register the container to the container
            this._serviceData.Add(typeof(IContainer), this);
        }

        public void Register<TInterface, TImplementation>(RegistrationOptions? options = null) where TImplementation : TInterface {

            Log.Trace(LogSeverity.Verbose, $"Registering {typeof(TInterface).Name} -> {typeof(TImplementation).Name} with options {options?.ToString()}");

            object serviceData = typeof(TImplementation);

            if (options?.Singleton == true) {
                serviceData = new SingletonEntry(typeof(TImplementation));
            }

            if (options?.Aggregate == true) {
                if (this.IsRegistered<TInterface>()) {
                    throw new ArgumentException($"Trying to register aggregate service but already found the non-aggregate service {this._serviceData[typeof(TInterface)].GetType().Name} registered already");
                }

                if (!this.IsRegistered<IEnumerable<TInterface>>()) {
                    this._serviceData.Add(typeof(IEnumerable<TInterface>), new List<object>());
                }

                object expectedAggregateEntry = this._serviceData[typeof(IEnumerable<TInterface>)];
                if (expectedAggregateEntry is not List<object> lst) {
                    throw new InvalidCastException($"Aggregate service value isn't a List: {this._serviceData[typeof(TInterface)].GetType().Name} -> {expectedAggregateEntry.GetType().Name}");
                }
                lst.Add(serviceData);
            } else {
                this._serviceData.Add(typeof(TInterface), serviceData);
            }
        }

        public bool IsRegistered<T>() {
            return this.IsRegistered(typeof(T));
        }

        public bool IsRegistered(Type type) {
            return this._serviceData.ContainsKey(type);
        }

        public T Resolve<T>() {
            return (T)this.Resolve(typeof(T));
        }

        public object Resolve(Type type) {
            if (!IsRegistered(type)) {
                throw new NullReferenceException($"No service registered to type: {type.Name}");
            }

            var entry = this._serviceData[type];

            if (type.IsAssignableTo(typeof(IEnumerable)) && type.IsGenericType) {
                type = type.GetGenericArguments().Single();
            }

            if (entry is List<object> enumerable) {
                return enumerable.Select(data => this.CreateInstanceOf(data)).MakeGeneric(type);
            }
            return this.CreateInstanceOf(entry);
        }

        private object CreateInstanceOf(object data) {

            if (data is SingletonEntry singletonEntry) {
                if (!singletonEntry.IsInstanciated) {
                    singletonEntry.Instance = this.CreateInstanceOf(singletonEntry.Type);
                }
                return singletonEntry.Instance!;
            }

            var type = (Type)data;
            var constructor = type.GetConstructors().Single();
            var dependencies = constructor.GetParameters()
                .Select(parameter => parameter.ParameterType)
                .Select(dependencyType => this.Resolve(dependencyType))
                .ToArray();
            return Activator.CreateInstance(type, dependencies)!;
        }

        public void Dispose() {
            foreach (var serviceData in this._serviceData.Values) {
                // Remember that 'this' is also injected into the container. Don't stackoverflow
                if (serviceData is IDisposable disposable && disposable != this) {
                    disposable.Dispose();
                }

                if (serviceData is IEnumerable<object> aggregateData) {
                    if (aggregateData is IDisposable disposableAggregate) {
                        disposableAggregate.Dispose();
                    }
                }
            }
        }
    }
}