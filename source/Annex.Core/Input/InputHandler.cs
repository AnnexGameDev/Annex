using Annex.Core.Data;
using Annex.Core.Graphics.Windows;
using Annex.Core.Hardware;
using Annex.Core.Input.InputEvents;
using Scaffold.Logging;

namespace Annex.Core.Input;

internal class InputHandler : IInputHandler
{
    private readonly IPlatformKeyboardService _platformKeyboardService;

    private readonly bool[] _keyboardKeyPressed;
    private readonly bool[] _mouseButtonStates;

    private bool InputShouldBeProcessed { get; set; } = true; // Window should be visible by default

    public InputHandler(IPlatformKeyboardService platformKeyboardService)
    {
        this._platformKeyboardService = platformKeyboardService;

        this._keyboardKeyPressed = new bool[Enum.GetValues<KeyboardKey>().Length];
        this._mouseButtonStates = new bool[Enum.GetValues<MouseButton>().Length];
    }

    public void HandleKeyboardKeyPressed(IWindow window, KeyboardKey key)
    {
        if (!InputShouldBeProcessed)
        {
            return;
        }

        Log.Debug($"KeyboardKey Pressed: {key}");

        if (key == KeyboardKey.Unknown)
        {
            return;
        }

        bool shift = this._platformKeyboardService.IsShiftPressed();
        bool capsLock = this._platformKeyboardService.IsCapsLockOn();
        bool ctrl = this._platformKeyboardService.IsControlPressed();
        var keyPressedEvent = new KeyboardKeyPressedEvent(window, key, shift, capsLock, ctrl);

        this._keyboardKeyPressed[(int)key] = true;
        window.Scene.OnKeyboardKeyPressed(window, keyPressedEvent);
    }

    public void HandleKeyboardKeyReleased(IWindow window, KeyboardKey key)
    {
        if (!InputShouldBeProcessed)
        {
            return;
        }

        Log.Debug($"KeyboardKey Released: {key}");

        if (key == KeyboardKey.Unknown)
        {
            return;
        }

        var keyReleasedEvent = new KeyboardKeyReleasedEvent(window, key);
        this._keyboardKeyPressed[(int)key] = false;
        window.Scene.OnKeyboardKeyReleased(window, keyReleasedEvent);
    }

    public void HandleWindowClosed(IWindow window)
    {
        Log.Normal($"Window closed: {window.Title}");
        window.Scene.OnWindowClosed(window);
    }

    public void HandleMouseButtonPressed(IWindow window, MouseButton button, Position position)
    {
        if (!InputShouldBeProcessed)
        {
            return;
        }

        // TODO: Track drag / dbl click
        Log.Debug($"MouseButton Pressed: {button} x:{position.X} y:{position.Y}");
        var mouseButtonPressedEvent = new MouseButtonPressedEvent(window, button, position.X, position.Y);
        this._mouseButtonStates[(int)button] = true;
        window.Scene.OnMouseButtonPressed(window, mouseButtonPressedEvent);
    }

    public void HandleMouseButtonReleased(IWindow window, MouseButton button, Position position)
    {
        if (!InputShouldBeProcessed)
        {
            return;
        }

        // TODO: Track drag / dbl click
        Log.Debug($"MouseButton Released: {button} x:{position.X} y:{position.Y}");
        var mouseButtonReleasedEvent = new MouseButtonReleasedEvent(window, button, position.X, position.Y);
        this._mouseButtonStates[(int)button] = false;
        window.Scene.OnMouseButtonReleased(window, mouseButtonReleasedEvent);
    }

    public void HandleMouseMoved(IWindow window, Position position)
    {
        if (!InputShouldBeProcessed)
        {
            return;
        }

        var mouseMovedEvent = new MouseMovedEvent(window, position.X, position.Y);
        window.Scene.OnMouseMoved(window, mouseMovedEvent);
    }

    public void HandleMouseScrollWheelMoved(IWindow window, double delta)
    {
        if (!InputShouldBeProcessed)
        {
            return;
        }

        var mouseScrollWheelMovedEvent = new MouseScrollWheelMovedEvent(window, delta);
        window.Scene.OnMouseScrollWheelMoved(window, mouseScrollWheelMovedEvent);
    }

    public bool IsKeyDown(KeyboardKey key)
    {
        if (!InputShouldBeProcessed)
        {
            return false;
        }

        return this._keyboardKeyPressed[(int)key];
    }

    public bool IsMouseButtonDown(MouseButton button)
    {
        if (!InputShouldBeProcessed)
        {
            return false;
        }

        return this._mouseButtonStates[(int)button];
    }

    public void HandleWindowGainedFocus(IWindow window)
    {
        Log.Normal($"Window gained focus");
        this.InputShouldBeProcessed = true;
        window.Scene.OnWindowGainedFocus(window);
    }

    public void HandleWindowLostFocus(IWindow window)
    {
        Log.Normal($"Window lost focus");
        this.InputShouldBeProcessed = false;
        window.Scene.OnWindowLostFocus(window);
    }
}