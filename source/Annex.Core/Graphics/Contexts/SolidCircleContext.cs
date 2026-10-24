using Annex.Core.Data;

namespace Annex.Core.Graphics.Contexts;

public class SolidCircleContext : DrawContext
{
    public RGBA FillColor { get; }
    public IVector2<float> Position { get; }
    public float Radius { get; }
    public IVector2<float>? RenderOffset { get; init; }
    public RGBA? BorderColor { get; init; }
    public float? BorderThickness { get; set; }

    public SolidCircleContext(RGBA color, IVector2<float> position, float radius)
    {
        FillColor = color;
        Position = position;
        Radius = radius;

        RenderOffset = null;
        BorderColor = null;
        BorderThickness = null;
    }
}
