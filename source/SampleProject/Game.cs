using Annex.Core;
using Annex.Core.Assets;
using Annex.Core.Graphics;
using Annex.Core.Logging;
using Annex.Core.Services;
using Annex.Sfml.Graphics;
using SampleProject.Scenes.Level1;
using System;
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
            container.Register<IGraphicsEngine, SfmlGraphicsEngine>();

            container.Register<Level1>();
        }

        protected override void SetupAssetBundles(IAssetService assetService) {
            string assetRoot = GetAssetRoot();
            string textureRoot = Path.Combine(assetRoot, "textures");
            //assetService.Textures.AddBundle(new FileSystemDirectory("*", textureRoot));
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
