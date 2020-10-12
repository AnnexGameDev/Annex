using Annex.Assets.Services;
using Annex.Events;
using Annex.Graphics;
using Annex.Logging;
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

        public static ILogService Log => ServiceContainerSingleton.Instance.Resolve<ILogService>();
        public static ISceneService SceneService => ServiceContainerSingleton.Instance.Resolve<ISceneService>();
        public static IEventService EventService => ServiceContainerSingleton.Instance.Resolve<IEventService>();
        public static ICanvas Canvas => ServiceContainerSingleton.Instance.Resolve<ICanvas>();

        public static ITextureManager TextureManager => ServiceContainerSingleton.Instance.Resolve<ITextureManager>();
        public static IAudioManager AudioManager => ServiceContainerSingleton.Instance.Resolve<IAudioManager>();
        public static IFontManager FontManager => ServiceContainerSingleton.Instance.Resolve<IFontManager>();
        public static IIconManager IconManager => ServiceContainerSingleton.Instance.Resolve<IIconManager>();
    }
}
