using Annex.Core;
using Annex.Core.Assets;
using Annex.Core.Assets.Bundles;
using Annex.Core.Graphics;
using Annex.Core.Services;
using Annex.Sfml.Graphics;
using System.IO;

namespace SampleProject
{
    public class Game : AnnexApp
    {
        private static void Main(string[] args) {
            new Game().Run();

            //container.Provide<IAudioManager>(new AudioManager());
            //container.Provide<IFontManager>(new FontManager());
            //container.Provide<IIconManager>(new IconManager());
            //container.Provide<IHtmlLayoutManager>(new HtmlLayoutManager());

            //container.Provide<IAudioService>(new SfmlPlayer());

            //Debug.PackageAssetsToBinary(ServiceProvider.AudioManager, Path.Combine(SolutionFolder, "assets/audio"));
            //Debug.PackageAssetsToBinary(ServiceProvider.FontManager, Path.Combine(SolutionFolder, "assets/fonts"));
            //Debug.PackageAssetsToBinary(ServiceProvider.IconManager, Path.Combine(SolutionFolder, "assets/icons"));
            //Debug.PackageAssetsToBinary(ServiceProvider.HtmlLayoutManager, Path.Combine(SolutionFolder, "assets/layouts"));
        }


        protected override void CreateWindow(IGraphicsService graphicsService) {
            var window = graphicsService.CreateWindow("MainWindow");
            window.IsVisible = true;
            window.WindowResolution.Set(960, 640);
            window.WindowSize.Set(960, 640);
        }

        protected override void RegisterTypes(IContainer container) {
            container.Register<IGraphicsEngine, SfmlGraphicsEngine>();
        }

        protected override void SetupAssetBundles(IAssetService assetService) {
            string assetRoot = GetAssetRoot();
            string textureRoot = Path.Combine(assetRoot, "textures");
            var textureBundle = new FileSystemDirectory("*", textureRoot);
            assetService.Textures.AddBundle(textureBundle);
        }

        private string GetAssetRoot() {
#if DEBUG        
            var root = Paths.GetParentFolderWithFile("Annex.sln");
#else
            var root = Paths.ApplicationPath;
#endif
            return Path.Combine(root, "assets");
        }
    }
}
