using Annex.Events;
using Annex.Graphics;
using Annex.Logging;
using Annex.Scenes;
using Annex.Services;

namespace AnnexSinglePlayer
{
    public class ServiceProvider
    {
        public static Log Log => ServiceContainerSingleton.Instance.Resolve<Log>();
        public static SceneService SceneService => ServiceContainerSingleton.Instance.Resolve<SceneService>();
        public static EventService EventService => ServiceContainerSingleton.Instance.Resolve<EventService>();
        public static ICanvas Canvas => ServiceContainerSingleton.Instance.Resolve<ICanvas>();
    }
}
