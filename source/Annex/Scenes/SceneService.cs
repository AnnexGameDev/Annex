using Annex.Assets;
using Annex.Scenes.Components;
using Annex.Services;
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
            this.LoadScene<Unknown>();
        }

        public T LoadScene<T>(bool createNewInstance = false) where T : Scene, new() {

            if (createNewInstance && _scenes.ContainsKey(typeof(T))) {
                UnloadScene<T>();
            }

            if (!this._scenes.ContainsKey(typeof(T))) {
                ServiceProvider.Log.WriteLineTrace(this, $"Creating new instance of scene {typeof(T).Name}");
                this._scenes[typeof(T)] = new T();
            }

            ServiceProvider.Log.WriteLineTrace(this, $"Loading scene {typeof(T).Name}");
            this._currentSceneType = typeof(T);
            return (T)this.CurrentScene;
        }

        public void UnloadScene<T>() where T : Scene {
            Debug.ErrorIf(this.CurrentScene.GetType() == typeof(T), "Unloading the current scene is prohibited");
            Debug.Assert(this._scenes.ContainsKey(typeof(T)), $"Tried to unload a scene {typeof(T).Name} that doesn't exist");
            ServiceProvider.Log.WriteLineTrace(this, $"Unloading instance of scene {typeof(T).Name}");
            _scenes.Remove(typeof(T));
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
