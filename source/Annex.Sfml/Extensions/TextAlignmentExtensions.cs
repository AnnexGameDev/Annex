using Annex.Core.Graphics.Contexts;

namespace Annex.Sfml.Extensions;

internal static class TextAlignmentExtensions
{
    public static float Align(this VerticalAlignment alignment, float height)
    {
        return alignment switch
        {
            VerticalAlignment.Top => 0,
            VerticalAlignment.Middle => height / 2f,
            VerticalAlignment.Bottom => height,
            _ => throw new InvalidOperationException($"Unknown vertical alignment: {alignment}")
        };
    }

    public static float Align(this HorizontalAlignment alignment, float width)
    {
        return alignment switch
        {
            HorizontalAlignment.Left => 0,
            HorizontalAlignment.Center => width / 2f,
            HorizontalAlignment.Right => width,
            _ => throw new InvalidOperationException($"Unknown horizontal alignment: {alignment}")
        };
    }
}
