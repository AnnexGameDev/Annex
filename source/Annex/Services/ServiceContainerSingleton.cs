namespace Annex.Services
{
    public partial class ServiceContainerSingleton
    {
        private static ServiceContainer? _instance;
        public static ServiceContainer Instance => _instance!;

        public static ServiceContainer Create() {
            if (_instance != null) {
                throw new AssertionFailedException($"{nameof(ServiceContainer)} singleton already exists");
            }
            _instance = new ServiceContainer();
            return Instance;
        }

        public static void Destroy() {
            if (_instance == null) {
                throw new AssertionFailedException($"{nameof(ServiceContainer)} singleton doesn't exist");
            }
            _instance.Dispose();
            _instance = null;
        }
    }
}
