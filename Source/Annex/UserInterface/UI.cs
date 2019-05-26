using Annex.UserInterface.Components;
using Annex.UserInterface.Scenes;
using System;
using System.Collections.Generic;

namespace Annex.UserInterface
{
    public class UI : Singleton
    {
        private readonly Dictionary<Type, Scene> _scenes;

        private Type _currentSceneType;
        public Scene CurrentScene => this._scenes[this._currentSceneType];

#pragma warning disable CS8618 // Non-nullable field is uninitialized.
        public UI() {           // Field is initialized in the LoadScene method.
#pragma warning restore CS8618 // Non-nullable field is uninitialized.
            this._scenes = new Dictionary<Type, Scene>();
            this.LoadScene<EmptyScene>();
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
