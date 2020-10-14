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
        private static ServiceContainer _instance;

        static ServiceProvider() {
            _instance = ServiceContainerSingleton.Instance;
        }

        public static void Destroy() {
            _instance.Dispose();
        }

        public static ILogService Log => _instance.Resolve<ILogService>()!;
        public static ISceneService SceneService => _instance.Resolve<ISceneService>()!;
        public static IEventService EventService => _instance.Resolve<IEventService>()!;
        public static ICanvas Canvas => _instance.Resolve<ICanvas>()!;

        public static ITextureManager TextureManager => _instance.Resolve<ITextureManager>()!;
        public static IAudioManager AudioManager => _instance.Resolve<IAudioManager>()!;
        public static IFontManager FontManager => _instance.Resolve<IFontManager>()!;
        public static IIconManager IconManager => _instance.Resolve<IIconManager>()!;
    }
}
