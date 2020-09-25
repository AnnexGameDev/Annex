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

        public T LoadNewScene<T>() where T : Scene, new() {
            if (_scenes.ContainsKey(typeof(T))) {
                ServiceProvider.Log.WriteLineTrace(this, $"Removing previous instance of scene {typeof(T).Name}");
                _scenes.Remove(typeof(T));
            }
            return LoadScene<T>();
        }

        public T LoadScene<T>() where T : Scene, new() {
            if (!this._scenes.ContainsKey(typeof(T))) {
                ServiceProvider.Log.WriteLineTrace(this, $"Creating new instance of scene {typeof(T).Name}");
                this._scenes[typeof(T)] = new T();
            }
            ServiceProvider.Log.WriteLineTrace(this, $"Loading scene {typeof(T).Name}");
            this._currentSceneType = typeof(T);
            return (T)this.CurrentScene;
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
