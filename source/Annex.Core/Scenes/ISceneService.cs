using Annex.Core.Scenes.Elements;

namespace Annex.Core.Scenes
{
    public interface ISceneService
    {
        IScene CurrentScene { get; }

        void LoadScene(IScene sceneInstance, object? parameters = null);
        void LoadScene<T>(object? parameters = null) where T : IScene;
        bool IsCurrentScene<T>() where T : IScene;
    }
}