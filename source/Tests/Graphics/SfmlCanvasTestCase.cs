#define DEBUG
using Annex;
using Annex.Assets;
using Annex.Events;
using Annex.Graphics;
using Annex.Graphics.Sfml;
using Annex.Logging;
using Annex.Scenes;
using Annex.Scenes.Components;
using System;
using System.IO;
using System.Threading;
using static Annex.Paths;

namespace Tests.Graphics
{
    public class SfmlCanvasTestCase
    {
        protected EventManager EventManager;
        protected ICanvas Canvas;
        protected SceneManager Scenes;

        private Thread _backgroundThread;
        private readonly string AssetFolder = Path.Combine(SolutionFolder, "assets/textures/");

        public SfmlCanvasTestCase() {
            ServiceProvider.Provide<Log>(new Log());
        }

        protected void StartTest<T>() where T : Scene, new() {
            this.EventManager = ServiceProvider.Provide<EventManager>();
            this.Scenes = ServiceProvider.Provide<SceneManager>();

            this._backgroundThread = new Thread(() => {
                this.Canvas = ServiceProvider.Provide<ICanvas>(new SfmlCanvas(new ServiceProvider.DefaultTextureManager(), new ServiceProvider.DefaultFontManager(), new ServiceProvider.DefaultIconManager()));
                AnnexGame.Initialize();
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
    }
}
