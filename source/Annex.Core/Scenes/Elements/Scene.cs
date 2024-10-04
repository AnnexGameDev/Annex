using Annex.Core.Data;
using Annex.Core.Events;
using Annex.Core.Graphics.Windows;
using Annex.Core.Input.InputEvents;

namespace Annex.Core.Scenes.Elements;

public class Scene : Container, IScene
{
    public IPriorityEventQueue Events { get; }

    /// <summary>
    /// The IUIElement that currently has the focus
    /// </summary>
    private IUIElement? _focusElement;
    public IUIElement? FocusElement
    {
        get => this._focusElement;
        // We don't want ourselves as the focus element. Otherwise we'll stackoverflow in the UI handlers
        private set => this._focusElement = value == this ? null : value;
    }

    public Scene(
        string elementId = "",
        IVector2<float>? size = null,
        IVector2<float>? position = null
        )
            : base(elementId, position ?? new Vector2f(), size ?? new Vector2f()) {
        this.Events = new PriorityEventQueue();
    }

    public virtual void OnEnter(OnSceneEnterEventArgs onSceneEnterEventArgs) {
    }

    public virtual void OnLeave(OnSceneLeaveEventArgs onSceneLeaveEventArgs) {
    }

    public virtual void OnKeyboardKeyPressed(IWindow window, KeyboardKeyPressedEvent keyboardKeyPressedEvent) {
        this.FocusElement?.OnKeyboardKeyPressed(keyboardKeyPressedEvent);
    }

    public virtual void OnKeyboardKeyReleased(IWindow window, KeyboardKeyReleasedEvent keyboardKeyReleasedEvent) {
        this.FocusElement?.OnKeyboardKeyReleased(keyboardKeyReleasedEvent);
    }

    public virtual void OnWindowClosed(IWindow window) {
    }

    public virtual void OnMouseButtonPressed(IWindow window, MouseButtonPressedEvent mouseButtonPressedEvent) {
        var newFocusElement = GetFirstVisibleElement(mouseButtonPressedEvent.WindowX, mouseButtonPressedEvent.WindowY);
        newFocusElement?.OnMouseButtonPressed(mouseButtonPressedEvent);

        if (this.FocusElement != newFocusElement)
        {
            this.FocusElement?.OnLostFocus();
            this.FocusElement = newFocusElement;
            this.FocusElement?.OnGainedFocus();
        }
    }

    public virtual void OnMouseButtonReleased(IWindow window, MouseButtonReleasedEvent mouseButtonReleasedEvent) {

        if (this.FocusElement?.IsInBounds(mouseButtonReleasedEvent.WindowX, mouseButtonReleasedEvent.WindowY) == true)
        {
            this.FocusElement?.OnMouseButtonReleased(mouseButtonReleasedEvent);
        }
    }

    private IUIElement? _lastMouseMovedElement = null;
    public virtual void OnMouseMoved(IWindow window, MouseMovedEvent mouseMovedEvent) {

        var newLastMovedElement = this.GetFirstVisibleElement(mouseMovedEvent.WindowX, mouseMovedEvent.WindowY);
        if (this._lastMouseMovedElement != newLastMovedElement)
        {
            this._lastMouseMovedElement?.OnMouseLeft(mouseMovedEvent);
        }
        this._lastMouseMovedElement = newLastMovedElement;
        this._lastMouseMovedElement?.OnMouseMoved(mouseMovedEvent);
    }

    public virtual void OnMouseScrollWheelMoved(IWindow window, MouseScrollWheelMovedEvent mouseScrollWheelMovedEvent) {
        var mousePosition = window.GetMousePos(Graphics.CameraId.UI);
        if (this.FocusElement?.IsInBounds(mousePosition.X, mousePosition.Y) == true)
        {
            this.FocusElement?.OnMouseScrollWheelMoved(mouseScrollWheelMovedEvent);
        }
    }

    protected override void Dispose(bool disposing) {
        base.Dispose(disposing);

        if (disposing)
        {
            this.Events.Dispose();
        }
    }

    public virtual void OnWindowGainedFocus() { }
    public virtual void OnWindowLostFocus() { }
}