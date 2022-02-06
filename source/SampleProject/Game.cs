using Annex.Core;
using Annex.Core.Services;

namespace SampleProject
{
    public class Game : AnnexApp
    {
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

        //public void Run() {
        //    ServiceProvider.SceneService.LoadScene<Level1>();
        //    ServiceProvider.Canvas.SetVisible(true);
        //    ServiceProvider.EventService.Run(this);
        //}

        protected override void RegisterTypes(IContainer container) {
        }
    }
}
