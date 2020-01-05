using Annex.Scenes.Components;
using System;
using System.Collections.Generic;

namespace Annex.Scenes
{
    public class SceneManager : IService
    {
        private readonly Dictionary<Type, Scene> _scenes;

        private Type _currentSceneType;
        public Scene CurrentScene => this._scenes[this._currentSceneType];

#pragma warning disable CS8618 // Non-nullable field is uninitialized.
        public SceneManager() {           // Field is initialized in the LoadScene method.
#pragma warning restore CS8618 // Non-nullable field is uninitialized.
            this._scenes = new Dictionary<Type, Scene>();

            // Even though the default scene is GameClosing, AnnexGame.Start<>() will override 
            // it before it causes the game loop to exit.
            this.LoadScene<GameClosing>();
        }

        public T LoadScene<T>(bool overwrite = false) where T : Scene, new() {
            if (overwrite || !this._scenes.ContainsKey(typeof(T))) {
                this._scenes[typeof(T)] = new T();
            }
            this._currentSceneType = typeof(T);
            return (T)this.CurrentScene;
        }

        public bool IsCurrentScene<T>() {
            return this.CurrentScene.GetType() == typeof(T);
        }

        public void Destroy() {

        }
    }
}
