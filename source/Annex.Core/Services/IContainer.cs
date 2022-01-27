namespace Annex.Core.Services
{
    public class RegistrationOptions
    {
        public bool Singleton { get; init; } = false;
    }

    public interface IContainer
    {
        void Register<TInterface, TImplementation>(RegistrationOptions? options = null) where TImplementation : TInterface;

        bool IsRegistered<T>();
        bool IsRegistered(Type type);
        T Resolve<T>();
        object Resolve(Type type);
    }
}