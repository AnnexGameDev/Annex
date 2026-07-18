using Annex.Core.Data;
using Annex.Core.Graphics.Contexts;
using Annex.Core.Graphics.Windows;
using Annex.Core.Input.InputEvents;

namespace Annex.Core.Scenes.Elements;

public class Button : UIElement, IButton
{
    private readonly Image _background;
    private readonly Label _label;

    public string? FocusedBackgroundTextureId
    {
        get => _background.FocusedBackgroundTextureId;
        set => _background.FocusedBackgroundTextureId = value;
    }

    public string? HoverBackgroundTextureId
    {
        get => _background.HoverBackgroundTextureId;
        set => _background.HoverBackgroundTextureId = value;
    }

    public string BackgroundTextureId
    {
        get => _background.BackgroundTextureId;
        set => _background.BackgroundTextureId = value;
    }
    public virtual string Text
    {
        get => _label.Text;
        set => _label.Text = value;
    }
    public string Font
    {
        get => _label.Font;
        set => _label.Font = value;
    }
    public uint FontSize
    {
        get => _label.FontSize;
        set => _label.FontSize = value;
    }
    public RGBA FontColor
    {
        get => _label.FontColor;
        set => _label.FontColor = value;
    }
    public HorizontalAlignment HorizontalTextAlignment
    {
        get => _label.HorizontalTextAlignment;
        set => _label.HorizontalTextAlignment = value;
    }
    public VerticalAlignment VerticalTextAlignment
    {
        get => _label.VerticalTextAlignment;
        set => _label.VerticalTextAlignment = value;
    }
    public IVector2<float> TextPositionOffset
    {
        get => _label.TextPositionOffset;
        set => _label.TextPositionOffset = value;
    }
    public float TextBorderThickness
    {
        get => _label.TextBorderThickness;
        set => _label.TextBorderThickness = value;
    }
    public RGBA TextBorderColor
    {
        get => _label.TextBorderColor;
        set => _label.TextBorderColor = value;
    }

    public Button(UIElementCreationArgs? args, IVector2<float>? textOffset = null, string? text = null) : base(args)
    {
        _background = new Image(new ($"{args?.ElementId}.background", Position, Size));
        _label = new Label(new ($"{args?.ElementId}.label", Position, Size), textOffset, text);
    }

    protected override void DrawInternal(IWindow window, long timeDelta)
    {
        _background.DrawOn(window, timeDelta);
        _label.DrawOn(window, timeDelta);
    }

    public override void OnMouseLeft(MouseMovedEvent mouseMovedEvent)
    {
        base.OnMouseLeft(mouseMovedEvent);
        _background.OnMouseLeft(mouseMovedEvent);
    }

    public override void OnMouseMoved(MouseMovedEvent mouseMovedEvent)
    {
        base.OnMouseMoved(mouseMovedEvent);
        _background.OnMouseMoved(mouseMovedEvent);
    }
}
