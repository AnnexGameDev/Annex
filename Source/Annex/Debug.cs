using Annex.Scenes;
using Annex.Scenes.Components;
using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Annex
{
    public static class Debug
    {
        [Conditional("DEBUG")]
        public static void ToggleDebugOverlay() {
            var scene = SceneManager.Singleton.CurrentScene;

            if (scene.GetElementById("Debug-Overlay") == null) {
                scene.AddChild(new DebugOverlay("Debug-Overlay"));
            } else {
                scene.RemoveElementById("Debug-Overlay");
            }
        }

        [Conditional("DEBUG")]
        public static void DisplayInformation(Func<string> worker) {
            DebugOverlay._informationRetrievers.Add(worker);
        }

        [Conditional("DEBUG")]
        public static void Assert(bool condition, [CallerLineNumber] int line = 0, [CallerMemberName] string callingMethod = "unknown", [CallerFilePath] string filePath = "unknown") {
            if (!condition) {
                Debug.Log($"Assertion failed in {filePath} on line {line} in the function {callingMethod}.");
                throw new System.Exception("Assertion failed.");
            }
        }

        [Conditional("DEBUG")]
        public static void Log(string line) {
            Logging.Log.Singleton.WriteLine(line);
        }
    }
}
