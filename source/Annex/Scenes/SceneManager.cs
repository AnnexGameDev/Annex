using Annex.Assets;
using Annex.Scenes.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Annex.Scenes
{
    public class SceneManager : IService
    {
        private readonly Dictionary<Type, Scene> _scenes;

        private Type _currentSceneType;
        public Scene CurrentScene => this._scenes[this._currentSceneType];

        public SceneManager() {
            this._scenes = new Dictionary<Type, Scene>();
            this.LoadScene<Unknown>();
        }

        public T LoadScene<T>(bool overwrite = false) where T : Scene, new() {
            if (overwrite || !this._scenes.ContainsKey(typeof(T))) {
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
