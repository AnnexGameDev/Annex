#define DEBUG
using Annex;
using Annex.Assets;
using Annex.Assets.Loaders;
using Annex.Assets.Managers;
using Annex.Events;
using Annex.Graphics;
using Annex.Graphics.Sfml;
using Annex.Logging;
using Annex.Scenes;
using Annex.Scenes.Components;
using Annex.Services;
using System;
using System.IO;
using System.Threading;
using static Annex.Paths;

namespace Tests.Graphics
{
    public class SfmlCanvasTestCase : TestWithServiceContainerSingleton
    {
        protected EventService EventManager;
        protected ICanvas Canvas;
        protected SceneService Scenes;

        private Thread _backgroundThread;
        private readonly string AssetFolder = Path.Combine(SolutionFolder, "assets/textures/");

        protected void StartTest<T>() where T : Scene, new() {
            ServiceContainer.Provide<Log>(new Log());
            this.EventManager = ServiceContainer.Provide<EventService>();
            this.Scenes = ServiceContainer.Provide<SceneService>();

            this._backgroundThread = new Thread(() => {
                this.Canvas = ServiceContainer.Provide<ICanvas>(new SfmlCanvas(new DefaultTextureManager(), new DefaultFontManager(), new DefaultIconManager()));
                Debug.PackageAssetsToBinaryFrom(AssetType.Texture, AssetFolder);
                AnnexGame.Start<T>();
                Console.WriteLine("Done!");
            });
            this._backgroundThread.Start();

            while (!this.Scenes.IsCurrentScene<T>()) {
                Thread.Yield();
            }
        }

        protected void EndTest() {
            Scenes.LoadGameClosingScene();
            this._backgroundThread.Join();
        }

        protected void Wait(int ms) {
            Thread.Sleep(ms);
        }

        private class DefaultTextureManager : CachedAssetManager
        {
            public DefaultTextureManager() : base(AssetType.Texture, new FileLoader(), new SfmlTextureInitializer("textures/")) {
            }
        }

        private class DefaultFontManager : CachedAssetManager
        {
            public DefaultFontManager() : base(AssetType.Font, new FileLoader(), new SfmlFontInitializer("fonts/")) {
            }
        }

        private class DefaultIconManager : CachedAssetManager
        {
            public DefaultIconManager() : base(AssetType.Icon, new FileLoader(), new SfmlFontInitializer("icons/")) {
            }
        }
    }
}
