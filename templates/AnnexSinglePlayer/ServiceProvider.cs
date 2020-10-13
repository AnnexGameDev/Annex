using Annex.Assets.Services;
using Annex.Events;
using Annex.Graphics;
using Annex.Logging;
using Annex.Scenes;
using Annex.Services;

namespace AnnexSinglePlayer
{
    public class ServiceProvider
    {
        private static ServiceContainer _singleton;

        static ServiceProvider() {
            _singleton = ServiceContainerSingleton.Instance;
        }

        public static void Destroy() {
            _singleton.Dispose();
        }

        public static ILogService Log => _singleton.Resolve<ILogService>();
        public static ISceneService SceneService => _singleton.Resolve<ISceneService>();
        public static IEventService EventService => _singleton.Resolve<IEventService>();
        public static ICanvas Canvas => _singleton.Resolve<ICanvas>();

        public static ITextureManager TextureManager => _singleton.Resolve<ITextureManager>();
        public static IAudioManager AudioManager => _singleton.Resolve<IAudioManager>();
        public static IFontManager FontManager => _singleton.Resolve<IFontManager>();
        public static IIconManager IconManager => _singleton.Resolve<IIconManager>();
    }
}
