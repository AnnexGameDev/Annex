using Annex.Core;
using Annex.Core.Assets;
using Annex.Core.Assets.Bundles;
using Annex.Core.Graphics;
using Annex.Sfml.Graphics;
using SampleProject.Scenes.Level1;
using Scaffold.DependencyInjection;
using Scaffold.Logging;
using System.IO;

namespace SampleProject
{
    public class Game : AnnexApp
    {
        private static void Main(string[] args) {
#if !DEBUG
            try {
#endif
            new Game().Run<Level1>();
#if !DEBUG
            } catch (Exception e) {
                Log.Trace(LogSeverity.Error, "Exception in main", exception: e);
            }
#endif
        }

        protected override void CreateWindow(IGraphicsService graphicsService) {
            var window = graphicsService.CreateWindow("MainWindow");
            window.IsVisible = true;
            window.WindowResolution.Set(960, 640);
            window.WindowSize.Set(960, 640);
        }

        protected override void RegisterTypes(IContainer container) {
            base.RegisterTypes(container);

            container.Register<IGraphicsEngine, SfmlGraphicsEngine>();
            container.Register<Level1>();
        }

        protected override void SetupAssetBundles(IAssetService assetService) {
            string assetRoot = GetAssetRoot();
            string textureRoot = Path.Combine(assetRoot, "textures");
            string fontsRoot = Path.Combine(assetRoot, "fonts");

#if DEBUG
            assetService.Textures.AddBundle(new PakFileBundle("textures.pak", "*.png", textureRoot));
            assetService.Fonts.AddBundle(new PakFileBundle("fonts.pak", "*.ttf", fontsRoot));
#else
            assetService.Textures.AddBundle(new PakFileBundle("texture.pak"));
            assetService.Fonts.AddBundle(new PakFileBundle("fonts.pak"));
#endif

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
