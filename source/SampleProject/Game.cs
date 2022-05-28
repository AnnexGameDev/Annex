using Annex_Old.Core;
using Annex_Old.Core.Assets;
using Annex_Old.Core.Assets.Bundles;
using Annex_Old.Core.Graphics;
using Annex_Old.Core.Networking;
using Annex_Old.Core.Networking.Engines.DotNet;
using Annex_Old.Sfml.Graphics;
using SampleProject.Scenes.Level1;
using SampleProject.Scenes.Level2;
using SampleProject.Scenes.Level3;
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
            using var game = new Game();
            game.Run<Level2>();
#if !DEBUG
            } catch (Exception e) {
                Log.Trace(LogSeverity.Error, "Exception in main", exception: e);
            }
#endif
        }

        protected override void CreateWindow(IGraphicsService graphicsService, IAssetService assetService) {
            var window = graphicsService.CreateWindow("MainWindow");
            window.IsVisible = true;
            window.SetResolution(960, 640);
            window.SetSize(960, 640);

            var icon = assetService.Textures.GetAsset("icons/icon.png")!;
            var cursor = assetService.Textures.GetAsset("cursors/cursor.png")!;

            window.SetIcon(100, 100, icon);
            window.SetMouseImage(cursor, 16, 16, 0, 0);
        }

        protected override void RegisterTypes(IContainer container) {
            base.RegisterTypes(container);

            container.Resolve<ILogService>().Filter.SetSeverity(LogSeverity.Verbose, false);

            container.RegisterSingleton<INetworkingEngine, DotNetNetworkingEngine>();
            container.RegisterSingleton<IGraphicsEngine, SfmlGraphicsEngine>();
            container.Register<Level1>();
            container.Register<Level2>();
            container.Register<Level3>();
        }

        protected override void SetupAssetBundles(IAssetService assetService) {
            string assetRoot = GetAssetRoot();
            string textureRoot = Path.Combine(assetRoot, "textures");
            string fontsRoot = Path.Combine(assetRoot, "fonts");
            string sceneDataRoot = Path.Combine(assetRoot, "scenes");

#if DEBUG
            assetService.Textures.AddBundle(new PakFileBundle("textures.pak", "*.png", textureRoot));
            assetService.Fonts.AddBundle(new PakFileBundle("fonts.pak", "*.ttf", fontsRoot));
            assetService.SceneData.AddBundle(new FileSystemBundle("*.html", sceneDataRoot));
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
