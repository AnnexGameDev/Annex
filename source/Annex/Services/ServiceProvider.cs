using Annex.Assets.Services;
using Annex.Events;
using Annex.Graphics;
using Annex.Logging;
using Annex.Scenes;

namespace Annex.Services
{
    internal class ServiceProvider
    {
        private static ServiceContainer _instance;

        static ServiceProvider() {
            _instance = ServiceContainerSingleton.Instance!;
        }

        internal static ILogService? LogService => _instance.Resolve<ILogService>();
        internal static ISceneService SceneService => _instance.Resolve<ISceneService>()!;
        internal static IEventService EventService => _instance.Resolve<IEventService>()!;
        internal static ICanvas Canvas => _instance.Resolve<ICanvas>()!;

        internal static ITextureManager TextureManager => _instance.Resolve<ITextureManager>()!;
        internal static IAudioManager AudioManager => _instance.Resolve<IAudioManager>()!;
        internal static IFontManager FontManager => _instance.Resolve<IFontManager>()!;
        internal static IIconManager IconManager => _instance.Resolve<IIconManager>()!;
    }
}
