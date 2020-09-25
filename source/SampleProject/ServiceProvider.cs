using Annex.Graphics;
using Annex.Scenes;
using Annex.Services;

namespace SampleProject
{
    public static class ServiceProvider
    {
        private static ServiceContainer _singleton;

        static ServiceProvider() {
            _singleton = ServiceContainerSingleton.Instance;
        }

        public static void Destroy() {
            _singleton.Dispose();
        }

        public static SceneService SceneService => _singleton.Resolve<SceneService>();
        public static ICanvas Canvas => _singleton.Resolve<ICanvas>();
    }
}
