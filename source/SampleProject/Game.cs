using Annex.Core;
using Annex.Core.Data;
using Annex.Core.Graphics;
using Annex.Core.Graphics.Windows;
using Annex.Core.Services;
using Annex.Sfml.Graphics;

namespace SampleProject
{
    public class Game : AnnexApp
    {
        private IGraphicsService _graphicsService;

        private static void Main(string[] args) {
            new Game().Run();

            //var container = ServiceContainerSingleton.Create();
            //container.Provide<ILogService>(new LogService());
            //ServiceProvider.Log.WriteLineTrace_Module("AnnexGame", "Initializing services...");

            //container.Provide<ITextureManager>(new TextureManager());
            //container.Provide<IAudioManager>(new AudioManager());
            //container.Provide<IFontManager>(new FontManager());
            //container.Provide<IIconManager>(new IconManager());
            //container.Provide<IHtmlLayoutManager>(new HtmlLayoutManager());

            //container.Provide<IEventService>(new EventService());
            //container.Provide<IAudioService>(new SfmlPlayer());
            //container.Provide<ICanvas>(new SfmlCanvas());
            //container.Provide<ISceneService>(new SceneService());

            //Debug.PackageAssetsToBinary(ServiceProvider.TextureManager, Path.Combine(SolutionFolder, "assets/textures"));
            //Debug.PackageAssetsToBinary(ServiceProvider.AudioManager, Path.Combine(SolutionFolder, "assets/audio"));
            //Debug.PackageAssetsToBinary(ServiceProvider.FontManager, Path.Combine(SolutionFolder, "assets/fonts"));
            //Debug.PackageAssetsToBinary(ServiceProvider.IconManager, Path.Combine(SolutionFolder, "assets/icons"));
            //Debug.PackageAssetsToBinary(ServiceProvider.HtmlLayoutManager, Path.Combine(SolutionFolder, "assets/layouts"));

            //ServiceContainerSingleton.Destroy();
        }


        protected override void Run() {
            var window = this._graphicsService.CreateWindow("MainWindow");
            window.IsVisible = true;
            window.WindowResolution.Set(960, 640);
            window.WindowSize.Set(960, 640);
            base.Run();
        }

        protected override void RegisterTypes(IContainer container) {
            container.Register<IGraphicsEngine, GraphicsEngine>();
            this._graphicsService = container.Resolve<IGraphicsService>();
        }
    }
}
