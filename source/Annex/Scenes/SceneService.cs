using Annex.Assets;
using Annex.Scenes.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Annex.Scenes
{
    public class SceneService : IService
    {
        private readonly Dictionary<Type, Scene> _scenes;

        private Type _currentSceneType;
        public Scene CurrentScene => this._scenes[this._currentSceneType];

        public SceneService() {
            this._scenes = new Dictionary<Type, Scene>();
            this._scenes[typeof(Unknown)] = new Unknown();
            this._currentSceneType = typeof(Unknown);
        }

        public T LoadNewScene<T>() where T : Scene, new() {
            Scene? destroyedSceneInstance = null;

            if (_scenes.ContainsKey(typeof(T))) {
                ServiceProvider.Log.WriteLineTrace(this, $"Removing previous instance of scene {typeof(T).Name}");

                if (this._currentSceneType == typeof(T)) {
                    destroyedSceneInstance = this._scenes[typeof(T)];
                }
                _scenes.Remove(typeof(T));
            }

            var previousScene = destroyedSceneInstance ?? this.CurrentScene;
            var nextScene = CreateSceneInstanceIfNotExists<T>();

            return SwitchScene<T>(previousScene, nextScene);
        }

        private T SwitchScene<T>(Scene previousScene, Scene nextScene) where T : Scene, new() {
            // OnSceneLeave
            previousScene.HandleSceneOnLeave(new SceneOnLeaveEvent() {
                NextScene = nextScene
            });

            ServiceProvider.Log.WriteLineTrace(this, $"Loading scene {nextScene.GetType().Name}");
            this._currentSceneType = typeof(T);

            // OnSceneEnter
            nextScene.HandleSceneOnEnter(new SceneOnEnterEvent() {
                PreviousScene = previousScene
            });

            return (T)nextScene;
        }

        private T CreateSceneInstanceIfNotExists<T>() where T : Scene, new() {
            if (!this._scenes.ContainsKey(typeof(T))) {
                ServiceProvider.Log.WriteLineTrace(this, $"Creating new instance of scene {typeof(T).Name}");
                this._scenes[typeof(T)] = new T();
            }
            return (T)this._scenes[typeof(T)];
        }

        public T LoadScene<T>() where T : Scene, new() {
            var previousScene = this.CurrentScene;
            var nextScene = CreateSceneInstanceIfNotExists<T>();
            return this.SwitchScene<T>(previousScene, nextScene);
        }

        public bool IsCurrentScene<T>() {
            return typeof(T) == this.CurrentScene.GetType();
        }

        public void LoadGameClosingScene() {
            this.LoadScene<GameClosing>();
        }

        public void Destroy() {

        }

        public IEnumerable<IAssetManager> GetAssetManagers() {
            return Enumerable.Empty<IAssetManager>();
        }
    }
}
