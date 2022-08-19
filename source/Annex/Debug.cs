using Annex_Old.Assets;
using Annex_Old.Scenes.Components;
using Annex_Old.Services;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using static Annex_Old.Paths;

namespace Annex_Old
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
        public static void WarnIf(bool condition, string message, [CallerLineNumber] int line = 0, [CallerMemberName] string callingMethod = "unknown", [CallerFilePath] string filePath = "unknown") {
            if (condition) {
                ServiceProvider.LogService?.WriteLineWarning(FormatAndLog(message, "Warning", line, callingMethod, filePath));
            }
        }

        [Conditional("DEBUG")]
        public static void Assert(bool condition, string reason, [CallerLineNumber] int line = 0, [CallerMemberName] string callingMethod = "unknown", [CallerFilePath] string filePath = "unknown") {
            if (!condition) {
                throw new AssertionFailedException(FormatAndLog(reason, "Failure", line, callingMethod, filePath));
            }
        }

        [Conditional("DEBUG")]
        public static void ErrorIf(bool condition, string reason, [CallerLineNumber] int line = 0, [CallerMemberName] string callingMethod = "unknown", [CallerFilePath] string filePath = "unknown") {
            if (condition) {
                throw new AssertionFailedException(FormatAndLog(reason, "Failure", line, callingMethod, filePath));
            }
        }

        [Conditional("DEBUG")]
        public static void Error(string reason, [CallerLineNumber] int line = 0, [CallerMemberName] string callingMethod = "unknown", [CallerFilePath] string filePath = "unknown") {
            throw new AssertionFailedException(FormatAndLog(reason, "Failure", line, callingMethod, filePath));
        }

        private static string FormatAndLog(string reason, string messageType, int line, string callingMethod, string filePath) {
            string message = $"{messageType} in {filePath.Substring(SourceFolder.Length)} on line {line} in the function {callingMethod}: {reason}";
            ServiceProvider.LogService?.WriteLineError(message);
            return message;
        }

        [Conditional("DEBUG")]
        public static void PackageAssetsToBinary(IAssetManager assetManager, string path) {

            path = path.Replace('\\', '/');
            if (!path.EndsWith("/")) {
                path += "/";
            }
            Directory.CreateDirectory(path);

            foreach (var uncleanFile in Directory.GetFiles(path, "*", SearchOption.AllDirectories)) {
                string file = uncleanFile.Replace('\\', '/');
                string extension = file.Substring(file.LastIndexOf("."));
                if (!assetManager.DataStreamer.IsValidExtension(extension)) {
                    continue;
                }

                string relativePath = file.Substring(path.Length);
                assetManager.DataStreamer.Write(relativePath, File.ReadAllBytes(file));
            }

            assetManager.DataStreamer.Persist();
        }
    }
}
