using Annex.Core.Graphics.Windows;
using Annex.Core.Input.InputEvents;
using Annex.Core.Input.Platforms;
using Annex.Core.Logging;
using Annex.Core.Scenes;

namespace Annex.Core.Input
{
    internal class InputHandlerService : IInputHandlerService
    {
        private readonly ISceneService _sceneService;
        private readonly IPlatformKeyboardService _platformKeyboardService;

        private IScene _currentScene => this._sceneService.CurrentScene;

        public InputHandlerService(ISceneService sceneService, IPlatformKeyboardService platformKeyboardService) {
            this._sceneService = sceneService;
            this._platformKeyboardService = platformKeyboardService;
        }

        public void HandleKeyboardKeyPressed(IWindow window, KeyboardKey key) {
            Log.Trace(LogSeverity.Verbose, $"KeyboardKey Pressed: {key}");

            bool shift = this._platformKeyboardService.IsShiftPressed();
            bool capsLock = this._platformKeyboardService.IsCapsLockOn();
            var keyPressedEvent = new KeyboardKeyPressedEvent(key, shift, capsLock);

            this._currentScene.OnKeyboardKeyPressed(window, keyPressedEvent);
        }

        public void HandleKeyboardKeyReleased(IWindow window, KeyboardKey key) {
            Log.Trace(LogSeverity.Verbose, $"KeyboardKey Released: {key}");
            var keyReleasedEvent = new KeyboardKeyReleasedEvent(key);
            this._currentScene.OnKeyboardKeyReleased(window, keyReleasedEvent);
        }

        public void HandleWindowClosed(IWindow window) {
            Log.Trace(LogSeverity.Verbose, $"Window closed: {window.Title}");
            this._currentScene.OnWindowClosed(window);
        }
    }
}