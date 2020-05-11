using Annex.Resources;
using Annex.Scenes.Components;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using static Annex.Strings.Paths;

namespace Annex
{
    public static class Debug
    {
        [Conditional("DEBUG")]
        public static void ToggleDebugOverlay() {
            var scene = ServiceProvider.SceneManager.CurrentScene;

            if (scene.GetElementById(DebugOverlay.ID) == null) {
                var overlay = new DebugOverlay();
                scene.AddChild(overlay);
                scene.FocusObject = overlay;
            } else {
                scene.FocusObject = null;
                scene.RemoveElementById(DebugOverlay.ID);
            }
        }

        [Conditional("DEBUG")]
        public static void AddDebugInformation(Func<string> worker) {
            DebugOverlay.AddInformation(worker);
        }

        [Conditional("DEBUG")]
        public static void AddDebugCommand(string commandName, Action<string[]> worker) {
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
            string message = $"Failure in {filePath.Substring(SolutionFolder.Length)} on line {line} in the function {callingMethod}: {reason}";
            ServiceProvider.Log.WriteLineError(message);
            return message;
        }

        [Conditional("DEBUG")]
        public static void PackageResourcesToBinary(ResourceType resourceType) {
            var di = new DirectoryInfo(".");
            string solutionPath;
            while (true) {
                if (Directory.GetFiles(di.FullName, "*.sln").Length != 0) {
                    solutionPath = di.FullName;
                    break;
                }
                di = di.Parent;
            }
            string resourcePath = Path.Combine(solutionPath, "resources");
            Directory.CreateDirectory(resourcePath);

            // TODO:
            //ServiceProvider.ResourceManagerRegistry.GetResourceManager(resourceType)?.PackageResourcesToBinary(resourcePath);
        }

        [Conditional("DEBUG")]
        public static void PackageResourcesToBinary() {
            PackageResourcesToBinary(ResourceType.Audio);
            PackageResourcesToBinary(ResourceType.Font);
            PackageResourcesToBinary(ResourceType.Textures);
        }
    }
}
