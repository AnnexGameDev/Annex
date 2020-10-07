using Annex.Scenes.Components;
using Annex.Services;

namespace Annex.Scenes
{
    public interface ISceneService : IService
    {
        Scene CurrentScene { get; }

        T LoadScene<T>(bool createNewInstance = false) where T : Scene, new();
        void UnloadScene<T>() where T : Scene;
        bool IsCurrentScene<T>() where T : Scene;
    }
}
