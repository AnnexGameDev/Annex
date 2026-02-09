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
    public IShared<float>? SuperSampleCount { get; init; }

    public IShared<string> Text { get; }
    public IShared<string> Font { get; }

    public IVector2<float>? Position { get; init; }
    public IVector2<float>? PositionOffset { get; init; }

    public IShared<uint>? FontSize { get; set; }
    public RGBA? Color { get; init; }

    public IShared<float>? BorderThickness { get; init; }
    public RGBA? BorderColor { get; init; }

    public IShared<float>? Rotation { get; init; }
    public HorizontalAlignment HorizontalAlignment { get; set; }
    public VerticalAlignment VerticalAlignment { get; set; }

    public TextContext(IShared<string> text, IShared<string> font)
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
