using System.Diagnostics;

namespace Annex.Core.Services
{
    internal class Container : IContainer
    {
        private readonly Dictionary<Type, object> _serviceData = new();

        private bool IsSingleton(object serviceData) => serviceData is not Type;

        public Container() {
            // Register the container to the container
            this._serviceData.Add(typeof(IContainer), this);
        }

        public void Register<TInterface, TImplementation>(RegistrationOptions? options = null) where TImplementation : TInterface {
            // Register the service as usual
            this._serviceData.Add(typeof(TInterface), typeof(TImplementation));

            if (options?.Singleton == true) {
                // If it's a singleton, then overwrite it with the instance.
                this._serviceData[typeof(TInterface)] = this.Resolve<TInterface>()!;
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

            var entry = this._serviceData[type];
            if (this.IsSingleton(entry)) {
                return entry;
            }

            Debug.Assert(entry is Type, "Non-singleton services should be stored as types");
            return CreateInstanceOf((Type)entry);
        }

        private object CreateInstanceOf(Type type) {
            var constructor = type.GetConstructors().Single();
            var dependencies = constructor.GetParameters()
                .Select(parameter => parameter.ParameterType)
                .Select(dependencyType => this.Resolve(dependencyType))
                .ToArray();
            return Activator.CreateInstance(type, dependencies)!;
        }
    }
}