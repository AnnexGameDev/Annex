using Annex.Scenes.Components;
using System;
using System.Collections.Generic;

namespace Annex.Scenes
{
    public class SceneManager : Singleton
    {
        private readonly Dictionary<Type, Scene> _scenes;

        private Type _currentSceneType;
        public Scene CurrentScene => this._scenes[this._currentSceneType];

        static SceneManager() {
            Create<SceneManager>();
        }
        public static SceneManager Singleton => Get<SceneManager>();

#pragma warning disable CS8618 // Non-nullable field is uninitialized.
        public SceneManager() {           // Field is initialized in the LoadScene method.
#pragma warning restore CS8618 // Non-nullable field is uninitialized.
            this._scenes = new Dictionary<Type, Scene>();

            // Even though the default scene is GameClosing, AnnexGame.Start<>() will override 
            // it before it causes the game loop to exit.
            this.LoadScene<GameClosing>();
        }

        public void LoadScene<T>() where T : Scene, new() {
            if (!this._scenes.ContainsKey(typeof(T))) {
                this._scenes.Add(typeof(T), new T());
            }
            this._currentSceneType = typeof(T);
        }

        public bool IsCurrentScene<T>() {
            return this.CurrentScene.GetType() == typeof(T);
        }
    }
}
