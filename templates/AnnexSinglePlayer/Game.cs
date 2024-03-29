﻿using Annex;
using Annex.Assets.Services;
using Annex.Audio;
using Annex.Audio.Sfml;
using Annex.Events;
using Annex.Graphics;
using Annex.Graphics.Sfml;
using Annex.Logging;
using Annex.Scenes;
using Annex.Services;
using AnnexSinglePlayer.Assets;
using AnnexSinglePlayer.Scenes.MainMenu;
using System.IO;
using static Annex.Paths;

namespace AnnexSinglePlayer
{
    public class Game : ITerminationCondition
    {
        private static void Main(string[] args) {
            var container = ServiceContainerSingleton.Create();
            container.Provide<ILogService>(new LogService());
            ServiceProvider.Log.WriteLineTrace_Module("AnnexGame", "Initializing services...");

            container.Provide<ITextureManager>(new TextureManager());
            container.Provide<IAudioManager>(new AudioManager());
            container.Provide<IFontManager>(new FontManager());
            container.Provide<IIconManager>(new IconManager());

            container.Provide<IEventService>(new EventService());
            container.Provide<IAudioService>(new SfmlPlayer());
            container.Provide<ICanvas>(new SfmlCanvas());
            container.Provide<ISceneService>(new SceneService());

            Debug.PackageAssetsToBinary(ServiceProvider.TextureManager, Path.Combine(SolutionFolder, "assets/textures/"));
            Debug.PackageAssetsToBinary(ServiceProvider.AudioManager, Path.Combine(SolutionFolder, "assets/audio/"));
            Debug.PackageAssetsToBinary(ServiceProvider.FontManager, Path.Combine(SolutionFolder, "assets/fonts/"));
            Debug.PackageAssetsToBinary(ServiceProvider.IconManager, Path.Combine(SolutionFolder, "assets/icons/"));

            new Game().Run();

            ServiceProvider.Destroy();
        }

        public void Run() {
            ServiceProvider.SceneService.LoadScene<MainMenuScene>();
            ServiceProvider.Canvas.SetVisible(true);
            ServiceProvider.EventService.Run(this);
        }

        public bool ShouldTerminate() {
            return false;
        }
    }
}
