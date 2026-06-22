using Annex.Core.Data;

namespace Annex.Core.Graphics.Contexts;

public enum HorizontalAlignment
{
    Left,
    Center,
    Right,
}

public enum VerticalAlignment
{
    Top,
    Middle,
    Bottom,
}

public class TextContext : DrawContext
{
    public float? SuperSampleCount { get; set; }

    public string Text { get; set; }
    public string Font { get; set; }

    public IVector2<float>? Position { get; init; }
    public IVector2<float>? PositionOffset { get; init; }

    public uint? FontSize { get; set; }
    public RGBA? Color { get; init; }

    public float? BorderThickness { get; set; }
    public RGBA? BorderColor { get; init; }

    public float? Rotation { get; set; }
    public HorizontalAlignment HorizontalAlignment { get; set; }
    public VerticalAlignment VerticalAlignment { get; set; }

    public TextContext(string text, string font)
    {
        Text = text;
        Font = font;

        Position = null;
        PositionOffset = null;
        FontSize = null;
        Color = null;
        BorderThickness = null;
        BorderColor = null;
        Rotation = null;

        VerticalAlignment = VerticalAlignment.Top;
        HorizontalAlignment = HorizontalAlignment.Left;
    }
}
