using Annex.Core.Data;
using Annex.Core.Graphics;
using Annex.Core.Input.InputEvents;

namespace Annex.Core.Scenes.Components;

public abstract class UIElement : IUIElement
{
    public string ElementID { get; set; }
    public IVector2<float> Size { get; }
    public IVector2<float> Position { get; }
    public bool Visible { get; set; }
    protected bool IsFocused { get; private set; }

    public UIElement(string? elementId = null, IVector2<float>? position = null, IVector2<float>? size = null) {
        this.ElementID = elementId ?? string.Empty;
        this.Position = position ?? new Vector2f();
        this.Size = size ?? new Vector2f();
        this.Visible = true;
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

    public void Draw(ICanvas canvas) {
        if (this.Visible)
            this.DrawInternal(canvas);
    }

    protected abstract void DrawInternal(ICanvas canvas);

    protected virtual void Dispose(bool disposing) {
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

    public void Dispose() {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    public bool IsInBounds(float x, float y) {
        if (x < this.Position.X || x > this.Position.X + this.Size.X)
            return false;
        if (y < this.Position.Y || y > this.Position.Y + this.Size.Y)
            return false;
        return true;
    }

    public virtual void OnLostFocus() {
        this.IsFocused = false;
        this.OnElementLostFocus?.Invoke(this, EventArgs.Empty);
    }

    public virtual void OnGainedFocus() {
        this.IsFocused = true;
        this.OnElementGainedFocus?.Invoke(this, EventArgs.Empty);
    }

    public virtual void OnMouseButtonPressed(MouseButtonPressedEvent mouseButtonPressedEvent) {
        this.OnElementMouseButtonPressed?.Invoke(this, mouseButtonPressedEvent);
    }

    public virtual void OnMouseButtonReleased(MouseButtonReleasedEvent mouseButtonReleasedEvent) {
        this.OnElementMouseButtonReleased?.Invoke(this, mouseButtonReleasedEvent);
    }

    public virtual void OnMouseMoved(MouseMovedEvent mouseMovedEvent) {
        this.OnElementMouseMoved?.Invoke(this, mouseMovedEvent);
    }

    public virtual void OnKeyboardKeyPressed(KeyboardKeyPressedEvent keyboardKeyPressedEvent) {
        this.OnElementKeyboardKeyPressed?.Invoke(this, keyboardKeyPressedEvent);
    }

    public virtual void OnKeyboardKeyReleased(KeyboardKeyReleasedEvent keyboardKeyReleasedEvent) {
        this.OnElementKeyboardKeyReleased?.Invoke(this, keyboardKeyReleasedEvent);
    }

    public virtual void OnMouseScrollWheelMoved(MouseScrollWheelMovedEvent mouseScrollWheelMovedEvent) {
        this.OnElementMouseScrollWheelMoved?.Invoke(this, mouseScrollWheelMovedEvent);
    }

    public virtual void OnMouseLeft(MouseMovedEvent mouseMovedEvent) {
        this.OnElementMouseLeft?.Invoke(this, mouseMovedEvent);
    }
}