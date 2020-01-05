#nullable enable
using Annex.Scenes;

namespace Annex
{
    public static partial class ServiceProvider
    {
        public static SceneManager SceneManager => Locate<SceneManager>() ?? Provide<SceneManager>(new SceneManager());
    }
}
