using Annex.Core.Data;
using Annex.Core.Graphics.Windows;
using Annex.Core.Input.InputEvents;
using Annex.Core.Input.Platforms;
using Annex.Core.Scenes;
using Annex.Core.Scenes.Elements;
using Scaffold.Logging;

namespace Annex.Core.Input
{
    internal class InputService : IInputService
    {
        private readonly ISceneService _sceneService;
        private readonly IPlatformKeyboardService _platformKeyboardService;

        private readonly bool[] _keyboardKeyPressed;
        private readonly bool[] _mouseButtonStates;

        private bool InputShouldBeProcessed { get; set; }

        private IScene _currentScene => this._sceneService.CurrentScene;

        public InputService(ISceneService sceneService, IPlatformKeyboardService platformKeyboardService) {
            this._sceneService = sceneService;
            this._platformKeyboardService = platformKeyboardService;

            this._keyboardKeyPressed = new bool[Enum.GetValues<KeyboardKey>().Length];
            this._mouseButtonStates = new bool[Enum.GetValues<MouseButton>().Length];
        }

        public void HandleKeyboardKeyPressed(IWindow window, KeyboardKey key) {
            if (!InputShouldBeProcessed)
            {
                return;
            }

            Log.Trace(LogSeverity.Verbose, $"KeyboardKey Pressed: {key}");

            if (key == KeyboardKey.Unknown)
            {
                return;
            }

            bool shift = this._platformKeyboardService.IsShiftPressed();
            bool capsLock = this._platformKeyboardService.IsCapsLockOn();
            bool ctrl = this._platformKeyboardService.IsControlPressed();
            var keyPressedEvent = new KeyboardKeyPressedEvent(key, shift, capsLock, ctrl);

            this._keyboardKeyPressed[(int)key] = true;
            this._currentScene.OnKeyboardKeyPressed(window, keyPressedEvent);
        }

        public void HandleKeyboardKeyReleased(IWindow window, KeyboardKey key) {
            if (!InputShouldBeProcessed)
            {
                return;
            }

            Log.Trace(LogSeverity.Verbose, $"KeyboardKey Released: {key}");

            if (key == KeyboardKey.Unknown)
            {
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

        public void HandleMouseButtonPressed(IWindow window, MouseButton button, IVector2<float> position) {
            if (!InputShouldBeProcessed)
            {
                return;
            }

            // TODO: Track drag / dbl click
            Log.Trace(LogSeverity.Verbose, $"MouseButton Pressed: {button}  x:{position.X}  y:{position.Y}");
            var mouseButtonPressedEvent = new MouseButtonPressedEvent(button, position.X, position.Y);
            this._mouseButtonStates[(int)button] = true;
            this._currentScene.OnMouseButtonPressed(window, mouseButtonPressedEvent);
        }

        public void HandleMouseButtonReleased(IWindow window, MouseButton button, IVector2<float> position) {
            if (!InputShouldBeProcessed)
            {
                return;
            }

            // TODO: Track drag / dbl click
            Log.Trace(LogSeverity.Verbose, $"MouseButton Released: {button}  x:{position.X}  y:{position.Y}");
            var mouseButtonReleasedEvent = new MouseButtonReleasedEvent(button, position.X, position.Y);
            this._mouseButtonStates[(int)button] = false;
            this._currentScene.OnMouseButtonReleased(window, mouseButtonReleasedEvent);
        }

        public void HandleMouseMoved(IWindow window, IVector2<float> position) {
            if (!InputShouldBeProcessed)
            {
                return;
            }

            Log.Trace(LogSeverity.Verbose, $"Mouse Moved: x:{position.X}  y:{position.Y}");
            var mouseMovedEvent = new MouseMovedEvent(position.X, position.Y);
            this._currentScene.OnMouseMoved(window, mouseMovedEvent);
        }

        public void HandleMouseScrollWheelMoved(IWindow window, double delta) {
            if (!InputShouldBeProcessed)
            {
                return;
            }

            Log.Trace(LogSeverity.Verbose, $"MouseScrollWheel Moved: {delta}");
            var mouseScrollWheelMovedEvent = new MouseScrollWheelMovedEvent(delta);
            this._currentScene.OnMouseScrollWheelMoved(window, mouseScrollWheelMovedEvent);
        }

        public bool IsKeyDown(KeyboardKey key) {
            if (!InputShouldBeProcessed)
            {
                return false;
            }

            return this._keyboardKeyPressed[(int)key];
        }

        public bool IsMouseButtonDown(MouseButton button) {
            if (!InputShouldBeProcessed)
            {
                return false;
            }

            return this._mouseButtonStates[(int)button];
        }

        public void HandleWindowGainedFocus() {
            Log.Trace(LogSeverity.Verbose, $"Window gained focus");
            this.InputShouldBeProcessed = true;
            this._currentScene.OnWindowGainedFocus();
        }

        public void HandleWindowLostFocus() {
            Log.Trace(LogSeverity.Verbose, $"Window lost focus");
            this.InputShouldBeProcessed = false;
            this._currentScene.OnWindowLostFocus();
        }
    }
}