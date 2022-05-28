using Annex_Old.Scenes.Components;
using Annex_Old.Services;

namespace Annex_Old.Scenes
{
    public interface ISceneService : IService
    {
        Scene CurrentScene { get; }

        T LoadScene<T>(bool createNewInstance = false) where T : Scene, new();
        void UnloadScene<T>() where T : Scene;
        bool IsCurrentScene<T>() where T : Scene;
    }
}
