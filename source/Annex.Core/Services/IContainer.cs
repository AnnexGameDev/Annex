namespace Annex.Core.Services
{
    public class RegistrationOptions
    {
        public bool Singleton { get; init; } = false;
        public bool Aggregate { get; init; } = false;

        public override string ToString() {
            return 
                $"{{ {Environment.NewLine}" +
                $"  {nameof(Singleton)}: {Singleton},{Environment.NewLine}" +
                $"  {nameof(Aggregate)}: {Aggregate},{Environment.NewLine}" +
                $"}}{Environment.NewLine}";
        }
    }

    public interface IContainer : IDisposable
    {
        void Register<TInterface, TImplementation>(RegistrationOptions? options = null) where TImplementation : TInterface;

        bool IsRegistered<T>();
        bool IsRegistered(Type type);
        T Resolve<T>();
        object Resolve(Type type);
    }
}