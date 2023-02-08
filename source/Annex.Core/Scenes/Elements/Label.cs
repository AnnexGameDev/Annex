using Annex.Core.Data;
using Annex.Core.Graphics;
using Annex.Core.Graphics.Contexts;

namespace Annex.Core.Scenes.Elements;

public class Label : UIElement, ILabel
{
    public readonly TextContext RenderText;

    public string Text
    {
        get => RenderText.Text.Value;
        set => RenderText.Text.Value = value;
    }
    public string Font
    {
        get => RenderText.Font.Value;
        set => RenderText.Font.Value = value;
    }
    public uint FontSize
    {
        get => RenderText.FontSize!.Value;
        set => RenderText.FontSize!.Value = value;
    }
    public RGBA FontColor
    {
        get => RenderText.Color!;
        set => RenderText.Color!.Set(value);
    }
    public HorizontalAlignment HorizontalTextAlignment
    {
        get => RenderText.HorizontalAlignment;
        set => RenderText.HorizontalAlignment = value;
    }
    public VerticalAlignment VerticalTextAlignment
    {
        get => RenderText.VerticalAlignment;
        set => RenderText.VerticalAlignment = value;
    }

    public IVector2<float> TextPositionOffset
    {
        get => RenderText.PositionOffset!;
        set => RenderText.PositionOffset!.Set(value);
    }
    public float TextBorderThickness
    {
        get => this.RenderText.BorderThickness?.Value ?? 0;
        set => this.RenderText.BorderThickness?.Set(value);
    }
    public RGBA TextBorderColor
    {
        get => this.RenderText.BorderColor ?? KnownColor.Transparent;
        set => this.RenderText.BorderColor?.Set(value);
    }

    public Label(string? elementId = null, IVector2<float>? position = null, IVector2<float>? size = null) : base(elementId, position, size) {
        this.RenderText = new TextContext(string.Empty, "default.ttf") {
            Position = this.Position,
            PositionOffset = new Vector2f(),
            Camera = CameraId.UI.ToString(),
            FontSize = new Shared<uint>(11),
            Color = KnownColor.Black,
            BorderThickness = new Shared<float>(0),
            BorderColor = KnownColor.Transparent
        };
    }

    protected override void DrawInternal(ICanvas canvas) {
        canvas.Draw(this.RenderText);
    }
}
