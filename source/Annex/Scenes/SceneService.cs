using Annex.Assets;
using Annex.Scenes.Components;
using Annex.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Annex.Scenes
{
    public class SceneService : ISceneService
    {
        private readonly Dictionary<Type, Scene> _scenes;

        private Type _currentSceneType;
        public Scene CurrentScene => this._scenes[this._currentSceneType];

        public SceneService() {
            this._scenes = new Dictionary<Type, Scene>();
            this._currentSceneType = typeof(Unknown);
            this._scenes.Add(typeof(Unknown), new Unknown());
        }

        public T LoadScene<T>(bool createNewInstance = false) where T : Scene, new() {
            var previousScene = this.CurrentScene;

            if (createNewInstance && _scenes.ContainsKey(typeof(T))) {
                UnloadScene<T>();
            }

            if (!this._scenes.ContainsKey(typeof(T))) {
                ServiceProvider.LogService?.WriteLineTrace(this, $"Creating new instance of scene {typeof(T).Name}");
                this._scenes[typeof(T)] = new T();
            }

            ServiceProvider.LogService?.WriteLineTrace(this, $"Loading scene {typeof(T).Name}");
            this._currentSceneType = typeof(T);

            previousScene.OnLeave(new OnSceneLeaveEvent(this.CurrentScene));
            this.CurrentScene.OnEnter(new OnSceneEnterEvent(previousScene));

            return (T)this.CurrentScene;
        }

        public void UnloadScene<T>() where T : Scene {
            Debug.Assert(this._scenes.ContainsKey(typeof(T)), $"Tried to unload a scene {typeof(T).Name} that doesn't exist");
            ServiceProvider.LogService?.WriteLineTrace(this, $"Unloading instance of scene {typeof(T).Name}");
            _scenes.Remove(typeof(T));
        }

        public bool IsCurrentScene<T>() where T : Scene {
            return typeof(T) == this.CurrentScene.GetType();
        }

        public void Destroy() {

        }
    }
}
