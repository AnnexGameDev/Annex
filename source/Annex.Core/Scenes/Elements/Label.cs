using Annex.Core.Data;
using Annex.Core.Graphics;
using Annex.Core.Graphics.Contexts;
using Annex.Core.Graphics.Windows;

namespace Annex.Core.Scenes.Elements;

public class Label : UIElement, ILabel
{
    public readonly TextContext RenderText;

    public virtual string Text
    {
        get => RenderText.Text;
        set => RenderText.Text = value;
    }
    public string Font
    {
        get => RenderText.Font;
        set => RenderText.Font = value;
    }
    public uint FontSize
    {
        get => RenderText.FontSize ?? 0;
        set => RenderText.FontSize = value;
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
        get => RenderText.BorderThickness ?? 0;
        set => RenderText.BorderThickness = value;
    }
    public RGBA TextBorderColor
    {
        get => RenderText.BorderColor ?? KnownColor.Transparent;
        set => RenderText.BorderColor?.Set(value);
    }

    public Label(string? elementId = null, IVector2<float>? position = null, IVector2<float>? size = null, IVector2<float>? textOffset = null, string? text = null)
        : base(elementId, position, size)
    {
        RenderText = new TextContext(text ?? string.Empty, "default.ttf")
        {
            Position = Position,
            PositionOffset = textOffset ?? new Vector2f(),
            Camera = KnownCamera.UI,
            FontSize = 12,
            Color = KnownColor.Black,
            BorderThickness = 0,
            BorderColor = KnownColor.Transparent,
        };
    }

    protected override void DrawInternal(IWindow window, long timeDelta)
    {
        window.Draw(RenderText);
    }
}
