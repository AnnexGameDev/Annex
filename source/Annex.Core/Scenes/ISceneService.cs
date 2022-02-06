﻿namespace Annex.Core.Scenes
{
    public interface ISceneService
    {
        IScene CurrentScene { get; }

        void LoadScene<T>() where T : IScene;
        bool IsCurrentScene<T>() where T : IScene;
    }
}