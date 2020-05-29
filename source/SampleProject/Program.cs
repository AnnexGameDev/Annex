using Annex;
using Annex.Assets;
using SampleProject.Scenes.Level1;
using System.IO;
using static Annex.Paths;

namespace SampleProject
{
    class Program
    {
        static void Main(string[] args) {
            AnnexGame.Initialize();
            Debug.PackageAssetsToBinaryFrom(AssetType.Audio, Path.Combine(SolutionFolder, "assets/audio/"));
            Debug.PackageAssetsToBinaryFrom(AssetType.Texture, Path.Combine(SolutionFolder, "assets/textures/"));
            Debug.PackageAssetsToBinaryFrom(AssetType.Font, Path.Combine(SolutionFolder, "assets/fonts/"));
            Debug.PackageAssetsToBinaryFrom(AssetType.Icon, Path.Combine(SolutionFolder, "assets/icons/"));

            ServiceProvider.Canvas.SetWindowIcon("icon.png");
            AnnexGame.Start<Level1>();
        }
    }
}