using Annex.Core.Data;
using Annex.Core.Graphics.Contexts;
using Annex.Core.Graphics.Windows;
using Annex.Core.Input.InputEvents;

namespace Annex.Core.Scenes.Elements;

public abstract class LabeledTextureUIElement : UIElement, IButton, ILabel
{
    protected readonly Image Image;
    protected readonly Label Label;

    public event EventHandler<TextChangedEventArgs>? OnTextChanged;

    public string? FocusedBackgroundTextureId
    {
        get => Image.FocusedBackgroundTextureId;
        set => Image.FocusedBackgroundTextureId = value;
    }
    public string? HoverBackgroundTextureId
    {
        get => Image.HoverBackgroundTextureId;
        set => Image.HoverBackgroundTextureId = value;
    }
    public string BackgroundTextureId
    {
        get => Image.BackgroundTextureId;
        set => Image.BackgroundTextureId = value;
    }
    public string Text
    {
        get => Label.Text;
        set
        {
            string oldText = Label.Text;
            Label.Text = value;
            if (oldText != value)
            {
                OnTextChanged?.Invoke(this, new TextChangedEventArgs(oldText));
            }
        }
    }
    public string Font
    {
        get => Label.Font;
        set => Label.Font = value;
    }
    public uint FontSize
    {
        get => Label.FontSize;
        set => Label.FontSize = value;
    }
    public RGBA FontColor
    {
        get => Label.FontColor;
        set => Label.FontColor = value;
    }
    public HorizontalAlignment HorizontalTextAlignment
    {
        get => Label.HorizontalTextAlignment;
        set => Label.HorizontalTextAlignment = value;
    }
    public VerticalAlignment VerticalTextAlignment
    {
        get => Label.VerticalTextAlignment;
        set => Label.VerticalTextAlignment = value;
    }
    public IVector2<float> TextPositionOffset
    {
        get => Label.TextPositionOffset;
        set => Label.TextPositionOffset = value;
    }
    public float TextBorderThickness
    {
        get => Label.TextBorderThickness;
        set => Label.TextBorderThickness = value;
    }
    public RGBA TextBorderColor
    {
        get => Label.TextBorderColor;
        set => Label.TextBorderColor = value;
    }

    public LabeledTextureUIElement(UIElementCreationArgs? args) : base(args)
    {
        Image = new Image(new ($"{args?.ElementId}.background", Position, Size));
        Label = new Label(new ($"{args?.ElementId}.label", Position, Size));
    }

    protected override void DrawInternal(IWindow window, long timeDelta)
    {
        Image.DrawOn(window, timeDelta);
        Label.DrawOn(window, timeDelta);
    }

    public override void OnLostFocus()
    {
        base.OnLostFocus();
        Label.OnLostFocus();
        Image.OnLostFocus();
    }

    public override void OnGainedFocus()
    {
        base.OnGainedFocus();
        Label.OnGainedFocus();
        Image.OnGainedFocus();
    }

    public override void OnMouseMoved(MouseMovedEvent mouseMovedEvent)
    {
        base.OnMouseMoved(mouseMovedEvent);
        Label.OnMouseMoved(mouseMovedEvent);
        Image.OnMouseMoved(mouseMovedEvent);
    }

    public override void OnMouseLeft(MouseMovedEvent mouseMovedEvent)
    {
        base.OnMouseLeft(mouseMovedEvent);
        Label.OnMouseLeft(mouseMovedEvent);
        Image.OnMouseLeft(mouseMovedEvent);
    }
}
