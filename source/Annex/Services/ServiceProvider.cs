using Annex_Old.Assets.Services;
using Annex_Old.Events;
using Annex_Old.Graphics;
using Annex_Old.Logging;
using Annex_Old.Scenes;

namespace Annex_Old.Services
{
    internal class ServiceProvider
    {
        private static ServiceContainer _instance => ServiceContainerSingleton.Instance!;

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
