using Annex_Old.Core.Graphics.Windows;
using Annex_Old.Core.Input.InputEvents;
using Annex_Old.Core.Input.Platforms;
using Annex_Old.Core.Scenes;
using Annex_Old.Core.Scenes.Components;
using Scaffold.Logging;

namespace Annex_Old.Core.Input
{
    internal class InputService : IInputService
    {
        private readonly ISceneService _sceneService;
        private readonly IPlatformKeyboardService _platformKeyboardService;

        private readonly bool[] _keyboardKeyPressed;
        private readonly bool[] _mouseButtonStates;

        private IScene _currentScene => this._sceneService.CurrentScene;

        public InputService(ISceneService sceneService, IPlatformKeyboardService platformKeyboardService) {
            this._sceneService = sceneService;
            this._platformKeyboardService = platformKeyboardService;

            this._keyboardKeyPressed = new bool[Enum.GetValues<KeyboardKey>().Length];
            this._mouseButtonStates = new bool[Enum.GetValues<MouseButton>().Length];
        }

        public void HandleKeyboardKeyPressed(IWindow window, KeyboardKey key) {
            Log.Trace(LogSeverity.Verbose, $"KeyboardKey Pressed: {key}");

            if (key == KeyboardKey.Unknown) {
                return;
            }

            bool shift = this._platformKeyboardService.IsShiftPressed();
            bool capsLock = this._platformKeyboardService.IsCapsLockOn();
            var keyPressedEvent = new KeyboardKeyPressedEvent(key, shift, capsLock);

            this._keyboardKeyPressed[(int)key] = true;
            this._currentScene.OnKeyboardKeyPressed(window, keyPressedEvent);
        }

        public void HandleKeyboardKeyReleased(IWindow window, KeyboardKey key) {
            Log.Trace(LogSeverity.Verbose, $"KeyboardKey Released: {key}");

            if (key == KeyboardKey.Unknown) {
                return;
            }

            var keyReleasedEvent = new KeyboardKeyReleasedEvent(key);
            this._keyboardKeyPressed[(int)key] = false;
            this._currentScene.OnKeyboardKeyReleased(window, keyReleasedEvent);
        }

        public void HandleWindowClosed(IWindow window) {
            Log.Trace(LogSeverity.Verbose, $"Window closed: {window.Title}");
            this._currentScene.OnWindowClosed(window);
        }

        public void HandleMouseButtonPressed(IWindow window, MouseButton button, int windowX, int windowY) {
            // TODO: Track drag / dbl click
            Log.Trace(LogSeverity.Verbose, $"MouseButton Pressed: {button}  x:{windowX}  y:{windowY}");
            var mouseButtonPressedEvent = new MouseButtonPressedEvent(button, windowX, windowY);
            this._mouseButtonStates[(int)button] = true;
            this._currentScene.OnMouseButtonPressed(window, mouseButtonPressedEvent);
        }

        public void HandleMouseButtonReleased(IWindow window, MouseButton button, int windowX, int windowY) {
            // TODO: Track drag / dbl click
            Log.Trace(LogSeverity.Verbose, $"MouseButton Released: {button}  x:{windowX}  y:{windowY}");
            var mouseButtonReleasedEvent = new MouseButtonReleasedEvent(button, windowX, windowY);
            this._mouseButtonStates[(int)button] = false;
            this._currentScene.OnMouseButtonReleased(window, mouseButtonReleasedEvent);
        }

        public void HandleMouseMoved(IWindow window, int windowX, int windowY) {
            Log.Trace(LogSeverity.Verbose, $"Mouse Moved: x:{windowX}  y:{windowY}");
            var mouseMovedEvent = new MouseMovedEvent(windowX, windowY);
            this._currentScene.OnMouseMoved(window, mouseMovedEvent);
        }

        public void HandleMouseScrollWheelMoved(IWindow window, double delta) {
            Log.Trace(LogSeverity.Verbose, $"MouseScrollWheel Moved: {delta}");
            var mouseScrollWheelMovedEvent = new MouseScrollWheelMovedEvent(delta);
            this._currentScene.OnMouseScrollWheelMoved(window, mouseScrollWheelMovedEvent);
        }

        public bool IsKeyDown(KeyboardKey key) {
            return this._keyboardKeyPressed[(int)key];
        }

        public bool IsMouseButtonDown(MouseButton button) {
            return this._mouseButtonStates[(int)button];
        }
    }
}