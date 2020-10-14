using Annex;
using Annex.Assets.Services;
using Annex.Audio;
using Annex.Audio.Sfml;
using Annex.Events;
using Annex.Graphics;
using Annex.Graphics.Sfml;
using Annex.Logging;
using Annex.Scenes;
using Annex.Scenes.Layouts.Html;
using Annex.Services;
using SampleProject.Assets;
using SampleProject.Scenes.Level1;
using System.IO;
using static Annex.Paths;

namespace SampleProject
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
            container.Provide<IHtmlLayoutManager>(new HtmlLayoutManager());

            container.Provide<IEventService>(new EventService());
            container.Provide<IAudioService>(new SfmlPlayer());
            container.Provide<ICanvas>(new SfmlCanvas());
            container.Provide<ISceneService>(new SceneService());

            Debug.PackageAssetsToBinary(ServiceProvider.TextureManager, Path.Combine(SolutionFolder, "assets/textures"));
            Debug.PackageAssetsToBinary(ServiceProvider.AudioManager, Path.Combine(SolutionFolder, "assets/audio"));
            Debug.PackageAssetsToBinary(ServiceProvider.FontManager, Path.Combine(SolutionFolder, "assets/fonts"));
            Debug.PackageAssetsToBinary(ServiceProvider.IconManager, Path.Combine(SolutionFolder, "assets/icons"));
            Debug.PackageAssetsToBinary(ServiceProvider.HtmlLayoutManager, Path.Combine(SolutionFolder, "assets/layouts"));

            new Game().Run();

            ServiceContainerSingleton.Destroy();
        }

        public void Run() {
            ServiceProvider.SceneService.LoadScene<Level1>();
            ServiceProvider.Canvas.SetVisible(true);
            ServiceProvider.EventService.Run(this);
        }

        private static bool _shouldTerminateFlag = false;
        
        public static void Terminate() {
            _shouldTerminateFlag = true;
        }

        public bool ShouldTerminate() {
            return _shouldTerminateFlag;
        }
    }
}
