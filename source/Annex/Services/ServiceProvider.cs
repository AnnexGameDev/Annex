using Annex.Events;
using Annex.Graphics;
using Annex.Logging;
using Annex.Scenes;

namespace Annex.Services
{
    internal class ServiceProvider
    {
        internal static Log Log => ServiceContainerSingleton.Instance.Resolve<Log>();
        internal static SceneService SceneService => ServiceContainerSingleton.Instance.Resolve<SceneService>();
        internal static EventService EventService => ServiceContainerSingleton.Instance.Resolve<EventService>();
        internal static ICanvas Canvas => ServiceContainerSingleton.Instance.Resolve<ICanvas>();
    }
}
