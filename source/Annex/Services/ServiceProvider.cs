using Annex.Events;
using Annex.Graphics;
using Annex.Logging;
using Annex.Scenes;

namespace Annex.Services
{
    internal class ServiceProvider
    {
        internal static ILogService LogService => ServiceContainerSingleton.Instance.Resolve<ILogService>();
        internal static ISceneService SceneService => ServiceContainerSingleton.Instance.Resolve<ISceneService>();
        internal static IEventService EventService => ServiceContainerSingleton.Instance.Resolve<IEventService>();
        internal static ICanvas Canvas => ServiceContainerSingleton.Instance.Resolve<ICanvas>();
    }
}
