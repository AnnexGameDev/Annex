using Annex.Resources;
using Annex.Scenes;
using Annex.Scenes.Components;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;

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
        public static void Assert(bool condition, [CallerLineNumber] int line = 0, [CallerMemberName] string callingMethod = "unknown", [CallerFilePath] string filePath = "unknown") {
            if (!condition) {
                string message = $"Assertion failed in {filePath} on line {line} in the function {callingMethod}.";
                Debug.Log(message);
                throw new System.Exception(message);
            }
        }

        [Conditional("DEBUG")]
        public static void Log(string line) {
            ServiceProvider.Log.WriteLine(line);
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

            ServiceProvider.ResourceManagerRegistry.GetResourceManager(resourceType)?.PackageResourcesToBinary(resourcePath);
        }

        [Conditional("DEBUG")]
        public static void PackageResourcesToBinary() {
            PackageResourcesToBinary(ResourceType.Audio);
            PackageResourcesToBinary(ResourceType.Font);
            PackageResourcesToBinary(ResourceType.Textures);
        }
    }
}
