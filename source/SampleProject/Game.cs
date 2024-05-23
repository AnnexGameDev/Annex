using Annex.Core;
using Annex.Core.Assets;
using Annex.Core.Assets.Bundles;
using Annex.Core.Graphics;
using Annex.Core.Networking;
using Annex.Core.Networking.Engines.DotNet;
using Annex.Core.Networking.Packets;
using Annex.Sfml.Graphics;
using SampleProject.Scenes.Level1;
using SampleProject.Scenes.Level2;
using SampleProject.Scenes.Level3;
using SampleProject.Scenes.Level4;
using SampleProject.Scenes.ListViewExample;
using Scaffold.DependencyInjection;
using Scaffold.Logging;
using System.IO;
using System.Threading.Tasks;

namespace SampleProject
{
    public class Game : AnnexApp
    {
        private static async Task Main(string[] args) {
#if !DEBUG
            try {
#endif
            using var game = new Game();
            await game.RunAsync<ListViewExampleScene>();
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

            var textures = assetService.Textures();

            var icon = textures.GetAsset("icons/icon.png")!;
            var cursor = textures.GetAsset("cursors/cursor.png")!;

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
            container.Register<Level4Scene>();
            container.Register<ListViewExampleScene>();

            container.RegisterAggregate<IPacketHandler, SimpleMessagePacketHandler>();
            container.RegisterAggregate<IPacketHandler, SimpleRequestPacketHandler>();
        }

        protected override void SetupAssetBundles(IAssetService assetService) {
            string assetRoot = GetAssetRoot();
            var textures = assetService.Textures();
            var fonts = assetService.Fonts();
            var sceneData = assetService.SceneData();

#if DEBUG
            string? textureRoot = Path.Combine(assetRoot, "textures");
            string? fontsRoot = Path.Combine(assetRoot, "fonts");
            string? sceneDataRoot = Path.Combine(assetRoot, "scenes");
#else
            string? textureRoot = null;
            string? fontsRoot = null;
            string? sceneDataRoot = null;
#endif
            textures.AddBundle(new PakFileBundle("textures.pak", "*.png", textureRoot));
            fonts.AddBundle(new PakFileBundle("fonts.pak", "*.ttf", fontsRoot));
            sceneData.AddBundle(new FileSystemBundle("*.html", sceneDataRoot));
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
