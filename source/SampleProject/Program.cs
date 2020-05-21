using Annex;
using Annex.Assets;
using Annex.Events.Trackers;
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

            var tracker = new InvocationCounterTracker(1000);
            var e = ServiceProvider.EventService.GetEvent(Annex.Graphics.EventIDs.DrawGameEventID);
            e.AttachTracker(tracker);

            Debug.AddDebugOverlayInformation(() => $"FPS {tracker.LastCount}");

            Debug.AddDebugOverlayCommand("setfps", (args) => {
                if (args[0] == "0") {
                    e.SetInterval(0);
                } else {
                    e.SetInterval(1000 / int.Parse(args[0]));
                }
            });

            ServiceProvider.Canvas.SetWindowIcon("icon.png");
            AnnexGame.Start<Level1>();
        }
    }
}