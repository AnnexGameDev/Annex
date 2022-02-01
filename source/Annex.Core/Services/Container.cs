using System.Diagnostics;

namespace Annex.Core.Services
{
    internal class Container : IContainer
    {
        private record UninstanciatedSingleton(Type type) { }

        private readonly Dictionary<Type, object> _serviceData = new();

        // If a service is stored as a Type, it should be instanciated on each Resolve.
        // Otherwise, it's a singleton
        private bool ShouldBeInstanciated(object data) => data is Type || data is UninstanciatedSingleton;

        public Container() {
            // Register the container to the container
            this._serviceData.Add(typeof(IContainer), this);
        }

        public void Register<TInterface, TImplementation>(RegistrationOptions? options = null) where TImplementation : TInterface {

            object serviceData = typeof(TImplementation);

            if (options?.Singleton == true) {
                serviceData = new UninstanciatedSingleton(typeof(TInterface));
            }

            if (options?.Aggregate == true) {
                if (!this.IsRegistered<TInterface>()) {
                    this._serviceData.Add(typeof(IEnumerable<TInterface>), new List<object>());
                }

                if (this._serviceData[typeof(IEnumerable<TInterface>)] is not List<object> lst) {
                    throw new InvalidCastException($"Trying to register aggregate service but already found the non-aggregate service {this._serviceData[typeof(TInterface)].GetType().Name} registered already");
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
            return (T)Resolve(typeof(T));
        }

        public object Resolve(Type type) {
            if (!IsRegistered(type)) {
                throw new NullReferenceException($"No service registered to type: {type.Name}");
            }

            if (type == typeof(IEnumerable<>)) {
                type = type.GetGenericArguments().Single();
            }

            var entry = this._serviceData[type];

            if (entry is List<object> enumerable) {
                return enumerable.Select(data => this.CreateInstanceOf(data));
            }
            return this.CreateInstanceOf(entry);
        }

        private object CreateInstanceOf(object data) {

            if (!this.ShouldBeInstanciated(data)) {
                return data;
            }

            if (data is UninstanciatedSingleton uninstanciatedSingleton) {
                return this.CreateInstanceOf(uninstanciatedSingleton.type);
            }

            var type = (Type)data;
            var constructor = type.GetConstructors().Single();
            var dependencies = constructor.GetParameters()
                .Select(parameter => parameter.ParameterType)
                .Select(dependencyType => this.Resolve(dependencyType))
                .ToArray();
            return Activator.CreateInstance(type, dependencies)!;
        }
    }
}