using Annex.Core.Data;
using Annex.Core.Graphics;
using Annex.Core.Graphics.Contexts;

namespace Annex.Core.Scenes.Elements;

public abstract class LabeledTextureUIElement : UIElement, IButton, ILabel
{
    protected readonly Image Image;
    protected readonly Label Label;

    public string? HoverBackgroundTextureId
    {
        get => this.Image.HoverBackgroundTextureId;
        set => this.Image.HoverBackgroundTextureId = value;
    }
    public string BackgroundTextureId
    {
        get => this.Image.BackgroundTextureId;
        set => this.Image.BackgroundTextureId = value;
    }
    public string Text
    {
        get => this.Label.Text;
        set => this.Label.Text = value;
    }
    public string Font
    {
        get => this.Label.Font;
        set => this.Label.Font = value;
    }
    public uint FontSize
    {
        get => this.Label.FontSize;
        set => this.Label.FontSize = value;
    }
    public RGBA FontColor
    {
        get => this.Label.FontColor;
        set => this.Label.FontColor = value;
    }
    public HorizontalAlignment HorizontalTextAlignment
    {
        get => this.Label.HorizontalTextAlignment;
        set => this.Label.HorizontalTextAlignment = value;
    }
    public VerticalAlignment VerticalTextAlignment
    {
        get => this.Label.VerticalTextAlignment;
        set => this.Label.VerticalTextAlignment = value;
    }
    public IVector2<float> TextPositionOffset
    {
        get => this.Label.TextPositionOffset;
        set => this.Label.TextPositionOffset = value;
    }
    public float TextBorderThickness
    {
        get => this.Label.TextBorderThickness;
        set => this.Label.TextBorderThickness = value;
    }
    public RGBA TextBorderColor
    {
        get => this.Label.TextBorderColor;
        set => this.Label.TextBorderColor = value;
    }

    public LabeledTextureUIElement(string? elementId = null, IVector2<float>? position = null, IVector2<float>? size = null) : base(elementId, position, size) {

        this.Image = new Image($"{elementId}.background", this.Position, this.Size);
        this.Label = new Label($"{elementId}.label", this.Position, this.Size);
    }

    protected override void DrawInternal(ICanvas canvas) {
        this.Image.Draw(canvas);
        this.Label.Draw(canvas);
    }
}
