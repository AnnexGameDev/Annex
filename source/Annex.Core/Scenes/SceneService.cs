using Annex.Core.Scenes.Elements;
using Scaffold.DependencyInjection;
using Scaffold.Logging;

namespace Annex.Core.Scenes
{
    internal class SceneService : ISceneService
    {
        private IScene? _currentScene;
        private readonly IContainer _container;

        public IScene CurrentScene => this._currentScene ?? new NullScene();

        public bool IsCurrentScene<T>() where T : IScene => this._currentScene is T;

        public SceneService(IContainer container) {
            this._container = container;
        }

        public void LoadScene<T>(object? parameters = null) where T : IScene {
            Log.Trace(LogSeverity.Verbose, $"Loading scene {typeof(T).Name}");
            var newScene = this._container.Resolve<T>(false);
            var oldScene = this._currentScene;

            // If the new scene can't be resolved, don't switch.
            if (newScene == null)
            {
                Log.Trace(LogSeverity.Error, $"Unable to resolve scene {typeof(T).Name}.");
                return;
            }

            this.SwitchTo(newScene, parameters);
        }

        private void SwitchTo<T>(T newScene, object? parameters = null) where T : IScene {
            var oldScene = this._currentScene;

            var leavingSceneArgs = new OnSceneLeaveEventArgs(newScene);
            var enteringSceneArgs = new OnSceneEnterEventArgs(oldScene, parameters);

            this._currentScene?.OnLeave(leavingSceneArgs);
            this._currentScene = newScene;
            this.CurrentScene.OnEnter(enteringSceneArgs);

            oldScene?.Dispose();
        }

        public void LoadScene(IScene sceneInstance, object? parameters = null) {
            Log.Trace(LogSeverity.Verbose, $"Loading scene instance {sceneInstance.GetType().Name}");
            this.SwitchTo(sceneInstance, parameters);
        }

        private class NullScene : Scene
        {

        }
    }
}