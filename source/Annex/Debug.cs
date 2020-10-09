using Annex.Assets;
using Annex.Scenes.Components;
using Annex.Services;
using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using static Annex.Paths;

namespace Annex
{
    public static class Debug
    {
        [Conditional("DEBUG")]
        public static void ToggleDebugOverlay() {
            var scene = ServiceProvider.SceneService.CurrentScene;

            if (scene.GetElementById(DebugOverlay.ID) == null) {
                var overlay = new DebugOverlay();
                scene.AddChild(overlay);
                scene.ChangeFocusObject(overlay);
            } else {
                scene.ChangeFocusObject(null);
                scene.RemoveElementById(DebugOverlay.ID);
            }
        }

        [Conditional("DEBUG")]
        public static void AddDebugOverlayInformation(Func<string> worker) {
            DebugOverlay.AddInformation(worker);
        }

        [Conditional("DEBUG")]
        public static void AddDebugOverlayCommand(string commandName, Action<string[]> worker) {
            DebugOverlay.AddCommand(commandName, worker);
        }

        [Conditional("DEBUG")]
        public static void Assert(bool condition, string reason, [CallerLineNumber] int line = 0, [CallerMemberName] string callingMethod = "unknown", [CallerFilePath] string filePath = "unknown") {
            if (!condition) {
                throw new AssertionFailedException(FormatAndLog(reason, line, callingMethod, filePath));
            }
        }

        [Conditional("DEBUG")]
        public static void ErrorIf(bool condition, string reason, [CallerLineNumber] int line = 0, [CallerMemberName] string callingMethod = "unknown", [CallerFilePath] string filePath = "unknown") {
            if (condition) {
                throw new AssertionFailedException(FormatAndLog(reason, line, callingMethod, filePath));
            }
        }

        [Conditional("DEBUG")]
        public static void Error(string reason, [CallerLineNumber] int line = 0, [CallerMemberName] string callingMethod = "unknown", [CallerFilePath] string filePath = "unknown") {
            throw new AssertionFailedException(FormatAndLog(reason, line, callingMethod, filePath));
        }

        private static string FormatAndLog(string reason, int line, string callingMethod, string filePath) {
            string message = $"Failure in {filePath.Substring(SourceFolder.Length)} on line {line} in the function {callingMethod}: {reason}";
            ServiceProvider.LogService?.WriteLineError(message);
            return message;
        }

        [Conditional("DEBUG")]
        public static void PackageAssetsToBinaryFrom(AssetType assetType, string path) {
            foreach (var registeredService in ServiceContainerSingleton.Instance!.RegisteredServices) {
                foreach (var assetManager in registeredService.GetAssetManagers().Where(assetManager => assetManager.AssetType == assetType)) {
                    assetManager.PackageAssetsToBinaryFrom(path);
                }
            }
        }
    }
}
