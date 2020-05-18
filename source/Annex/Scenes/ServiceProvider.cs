using Annex.Scenes;

namespace Annex
{
    public static partial class ServiceProvider
    {
        public static SceneService SceneService => Locate<SceneService>() ?? Provide<SceneService>();
    }
}
