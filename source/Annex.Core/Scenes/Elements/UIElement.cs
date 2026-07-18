using Annex.Core.Data;
using Annex.Core.Graphics.Windows;
using Annex.Core.Input.InputEvents;

namespace Annex.Core.Scenes.Elements;

public abstract class UIElement : IUIElement
{
    public string ElementID { get; set; }
    public IVector2<float> Size { get; }
    public IVector2<float> Position { get; }
    public bool Visible { get; set; }
    protected bool IsFocused { get; private set; }
    public bool InputTransparent { get; set; } = false;

    public UIElement(UIElementCreationArgs? args)
    {
        ElementID = args?.ElementId ?? string.Empty;
        Position = args?.Position ?? new Vector2f();
        Size = args?.Size ?? new Vector2f();
        Visible = true;
    }
    private bool disposedValue = false;

    public event EventHandler? OnElementLostFocus;
    public event EventHandler? OnElementGainedFocus;
    public event EventHandler<MouseButtonPressedEvent>? OnElementMouseButtonPressed;
    public event EventHandler<MouseButtonReleasedEvent>? OnElementMouseButtonReleased;
    public event EventHandler<MouseMovedEvent>? OnElementMouseMoved;
    public event EventHandler<KeyboardKeyPressedEvent>? OnElementKeyboardKeyPressed;
    public event EventHandler<KeyboardKeyReleasedEvent>? OnElementKeyboardKeyReleased;
    public event EventHandler<MouseScrollWheelMovedEvent>? OnElementMouseScrollWheelMoved;
    public event EventHandler<MouseMovedEvent>? OnElementMouseLeft;
    public event EventHandler<MouseMovedEvent>? OnElementMouseEntered;

    public void DrawOn(IWindow window, long timeDelta)
    {
        if (Visible)
            DrawInternal(window, timeDelta);
    }

    protected abstract void DrawInternal(IWindow canvas, long timeDelta);

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                // TODO: dispose managed state (managed objects)
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            disposedValue = true;
        }
    }

    // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    // ~UIElement()
    // {
    //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
    //     Dispose(disposing: false);
    // }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    public bool IsInBounds(float x, float y)
    {
        if (x < Position.X || x > Position.X + Size.X)
            return false;
        if (y < Position.Y || y > Position.Y + Size.Y)
            return false;
        return !InputTransparent;
    }

    public virtual void OnLostFocus()
    {
        IsFocused = false;
        OnElementLostFocus?.Invoke(this, EventArgs.Empty);
    }

    public virtual void OnGainedFocus()
    {
        IsFocused = true;
        OnElementGainedFocus?.Invoke(this, EventArgs.Empty);
    }

    public virtual void OnMouseButtonPressed(MouseButtonPressedEvent mouseButtonPressedEvent)
    {
        OnElementMouseButtonPressed?.Invoke(this, mouseButtonPressedEvent);
    }

    public virtual void OnMouseButtonReleased(MouseButtonReleasedEvent mouseButtonReleasedEvent)
    {
        OnElementMouseButtonReleased?.Invoke(this, mouseButtonReleasedEvent);
    }

    public virtual void OnMouseMoved(MouseMovedEvent mouseMovedEvent)
    {
        OnElementMouseMoved?.Invoke(this, mouseMovedEvent);
    }

    public virtual void OnKeyboardKeyPressed(KeyboardKeyPressedEvent keyboardKeyPressedEvent)
    {
        OnElementKeyboardKeyPressed?.Invoke(this, keyboardKeyPressedEvent);
    }

    public virtual void OnKeyboardKeyReleased(KeyboardKeyReleasedEvent keyboardKeyReleasedEvent)
    {
        OnElementKeyboardKeyReleased?.Invoke(this, keyboardKeyReleasedEvent);
    }

    public virtual void OnMouseScrollWheelMoved(MouseScrollWheelMovedEvent mouseScrollWheelMovedEvent)
    {
        OnElementMouseScrollWheelMoved?.Invoke(this, mouseScrollWheelMovedEvent);
    }

    public virtual void OnMouseLeft(MouseMovedEvent mouseMovedEvent)
    {
        OnElementMouseLeft?.Invoke(this, mouseMovedEvent);
    }

    public virtual void OnMouseEntered(MouseMovedEvent mouseMovedEvent)
    {
        OnElementMouseEntered?.Invoke(this, mouseMovedEvent);
    }
}