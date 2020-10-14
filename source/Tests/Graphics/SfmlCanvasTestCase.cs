#define DEBUG
using Annex.Events;
using Annex.Graphics;
using Annex.Graphics.Sfml;
using Annex.Logging;
using Annex.Scenes;
using Annex.Scenes.Components;
using Annex.Services;
using NUnit.Framework;
using System;
using System.IO;
using System.Threading;
using static Annex.Paths;

namespace Tests.Graphics
{
    public class SfmlCanvasTestCase
    {
        protected IEventService EventManager;
        protected ICanvas Canvas;
        protected ISceneService Scenes;

        private Thread _backgroundThread;
        private readonly string AssetFolder = Path.Combine(SolutionFolder, "assets/textures/");

        private ServiceContainer ServiceContainer => ServiceContainerSingleton.Instance!;

        [OneTimeSetUp]
        public void OneTimeSetUp() {
            ServiceContainerSingleton.Create();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown() {
            ServiceContainerSingleton.Destroy();
        }

        protected void StartTest<T>() where T : Scene, new() {
            ServiceContainer.Provide<LogService>(new LogService());
            this.EventManager = ServiceContainer.Provide<IEventService>(new EventService());
            this.Scenes = ServiceContainer.Provide<ISceneService>(new SceneService());

            this._backgroundThread = new Thread(() => {
                this.Canvas = ServiceContainer.Provide<ICanvas>(new SfmlCanvas());
                // TODO: Commented out due to removal of AnnexGame, resulting in broken test cases
                //AnnexGame.Start<T>();
                Console.WriteLine("Done!");
            });
            this._backgroundThread.Start();

            while (!this.Scenes.IsCurrentScene<T>()) {
                Thread.Yield();
            }
        }

        protected void EndTest() {
            // TODO: Scenes.LoadGameClosingScene();
            this._backgroundThread.Join();
        }

        protected void Wait(int ms) {
            Thread.Sleep(ms);
        }
    }
}
