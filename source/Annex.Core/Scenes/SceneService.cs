﻿using Annex.Core.Scenes.Elements;
using Scaffold.DependencyInjection;
using Scaffold.Logging;

namespace Annex.Core.Scenes
{
    internal class SceneService : ISceneService
    {
        private IScene? _currentScene;
        private readonly IContainer _container;

        public IScene CurrentScene => this._currentScene ?? throw new NullReferenceException("Current scene is null");

        public bool IsCurrentScene<T>() where T : IScene => this._currentScene is T;

        public SceneService(IContainer container) {
            this._container = container;
        }

        public void LoadScene<T>() where T : IScene {
            Log.Trace(LogSeverity.Verbose, $"Loading scene {typeof(T).Name}");
            var newScene = this._container.Resolve<T>();
            var oldScene = this._currentScene;

            // If the new scene can't be resolved, don't switch.
            if (newScene == null) {
                Log.Trace(LogSeverity.Error, $"Unable to resolve scene {typeof(T).Name}.");
                return;
            }

            var leavingSceneArgs = new OnSceneLeaveEventArgs(newScene);
            var enteringSceneArgs = new OnSceneEnterEventArgs(oldScene);

            this._currentScene?.OnLeave(leavingSceneArgs);
            this._currentScene = newScene;
            this.CurrentScene.OnEnter(enteringSceneArgs);
        }

        public void LoadScene(IScene sceneInstance) {
            Log.Trace(LogSeverity.Verbose, $"Loading scene instance {sceneInstance.GetType().Name}");
            var newScene = sceneInstance;
            var oldScene = this._currentScene;

            var leavingSceneArgs = new OnSceneLeaveEventArgs(newScene);
            var enteringSceneArgs = new OnSceneEnterEventArgs(oldScene);

            this._currentScene?.OnLeave(leavingSceneArgs);
            this._currentScene = newScene;
            this.CurrentScene.OnEnter(enteringSceneArgs);
        }
    }
}