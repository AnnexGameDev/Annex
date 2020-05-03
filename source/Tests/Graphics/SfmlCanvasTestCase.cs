using Annex;
using Annex.Events;
using Annex.Graphics;
using Annex.Graphics.Sfml;
using Annex.Resources;
using Annex.Scenes;
using Annex.Scenes.Components;
using System;
using System.Threading;

namespace Tests.Graphics
{
    public class SfmlCanvasTestCase
    {
        protected EventManager EventManager;
        protected Canvas Canvas;
        protected SceneManager Scenes;

        private Thread _backgroundThread;

        protected void StartTest<T>() where T : Scene, new() {
            this.EventManager = ServiceProvider.Provide<EventManager>();
            this.Scenes = ServiceProvider.Provide<SceneManager>();

            this._backgroundThread = new Thread(() => {
                this.Canvas = ServiceProvider.Provide<Canvas, SfmlCanvas>();
                AnnexGame.Initialize();
                Debug.PackageResourcesToBinary(ResourceType.Textures);
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
