using Annex.Core.Scenes.Elements;

namespace Annex.Core.Scenes
{
    public interface ISceneService
    {
        IScene CurrentScene { get; }

        void LoadScene(IScene sceneInstance, params object[] parameters);
        void LoadScene<T>(params object[] parameters) where T : IScene;
        bool IsCurrentScene<T>() where T : IScene;
    }
}