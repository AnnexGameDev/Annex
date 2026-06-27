using Annex.Core.Assets;
using Annex.Core.Data;
using Annex.Core.Graphics;
using Annex.Core.Graphics.Windows;
using Annex.Core.Input.InputEvents;

namespace Annex.Core.Scenes.Elements;

public class Scene : Container, IScene
{
    public AssetRegistry Assets { get; } = new AssetRegistry();

    /// <summary>
    /// The IUIElement that currently has the focus
    /// </summary>
    public IUIElement? CurrentFocusElement
    {
        get;
        // We don't want ourselves as the focus element. Otherwise we'll stackoverflow in the UI handlers
        private set => field = value == this ? null : value;
    }

    public IUIElement? CurrentHoverElement
    {
        get;
        private set => field = value == this ? null : value;
    }

    public Scene(
        string elementId = "",
        IVector2<float>? size = null,
        IVector2<float>? position = null
        )
            : base(elementId, position ?? new Vector2f(), size ?? new Vector2f())
    {
    }

    public virtual void OnEnter(OnSceneEnterEventArgs onSceneEnterEventArgs)
    {
    }

    public virtual void OnLeave(OnSceneLeaveEventArgs onSceneLeaveEventArgs)
    {
    }

    public virtual void OnKeyboardKeyPressed(IWindow window, KeyboardKeyPressedEvent keyboardKeyPressedEvent)
    {
        CurrentFocusElement?.OnKeyboardKeyPressed(keyboardKeyPressedEvent);
    }

    public virtual void OnKeyboardKeyReleased(IWindow window, KeyboardKeyReleasedEvent keyboardKeyReleasedEvent)
    {
        CurrentFocusElement?.OnKeyboardKeyReleased(keyboardKeyReleasedEvent);
    }

    public virtual void OnWindowClosed(IWindow window)
    {
    }

    public virtual void OnMouseButtonPressed(IWindow window, MouseButtonPressedEvent mouseButtonPressedEvent)
    {
        var newFocusElement = GetFirstVisibleElement(mouseButtonPressedEvent.WindowX, mouseButtonPressedEvent.WindowY);
        newFocusElement?.OnMouseButtonPressed(mouseButtonPressedEvent);

        if (newFocusElement != null && newFocusElement is not IScene)
        {
            mouseButtonPressedEvent.Handled = true;
        }
        SetFocus(newFocusElement);
    }

    public virtual void OnMouseButtonReleased(IWindow window, MouseButtonReleasedEvent mouseButtonReleasedEvent)
    {
        if (CurrentFocusElement?.IsInBounds(mouseButtonReleasedEvent.WindowX, mouseButtonReleasedEvent.WindowY) == true)
        {
            CurrentFocusElement?.OnMouseButtonReleased(mouseButtonReleasedEvent);
        }
    }

    public virtual void OnMouseMoved(IWindow window, MouseMovedEvent mouseMovedEvent)
    {
        var newLastMovedElement = GetFirstVisibleElement(mouseMovedEvent.WindowX, mouseMovedEvent.WindowY);
        if (CurrentHoverElement != newLastMovedElement)
        {
            CurrentHoverElement?.OnMouseLeft(mouseMovedEvent);
        }
        CurrentHoverElement = newLastMovedElement;
        CurrentHoverElement?.OnMouseMoved(mouseMovedEvent);
    }

    public virtual void OnMouseScrollWheelMoved(IWindow window, MouseScrollWheelMovedEvent mouseScrollWheelMovedEvent)
    {
        var mousePosition = window.GetMousePos(KnownCamera.UI);
        if (CurrentFocusElement?.IsInBounds(mousePosition.X, mousePosition.Y) == true)
        {
            CurrentFocusElement?.OnMouseScrollWheelMoved(mouseScrollWheelMovedEvent);
        }
    }

    public virtual void OnWindowGainedFocus(IWindow window) { }
    public virtual void OnWindowLostFocus(IWindow window) { }

    public void SetFocus(IUIElement? newFocusElement)
    {
        if (CurrentFocusElement != newFocusElement)
        {
            CurrentFocusElement?.OnLostFocus();
            CurrentFocusElement = newFocusElement;
            CurrentFocusElement?.OnGainedFocus();
        }
    }

    public void AddChild(IUIElement element, bool focus = false)
    {
        base.AddChild(element);
        if (focus)
        {
            CurrentFocusElement = element;
        }
    }

    public override void RemoveChild(string elementId)
    {
        base.RemoveChild(elementId);

        if (CurrentFocusElement?.ElementID == elementId)
        {
            CurrentFocusElement = null;
        }
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);

        if (disposing)
        {
            Assets.Dispose();
        }
    }
}