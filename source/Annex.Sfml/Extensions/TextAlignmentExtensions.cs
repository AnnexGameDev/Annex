using Annex_Old.Core.Graphics.Contexts;
using SFML.Graphics;

namespace Annex_Old.Sfml.Extensions
{
    internal static class TextAlignmentExtensions
    {
        public static float Align(this VerticalAlignment alignment, FloatRect bounds) {
            return alignment switch {
                VerticalAlignment.Top => 0,
                VerticalAlignment.Middle => bounds.Height / 2f,
                VerticalAlignment.Bottom => bounds.Height,
                _ => throw new InvalidOperationException($"Unknown vertical alignment: {alignment}")
            };
        }

        public static float Align(this HorizontalAlignment alignment, FloatRect bounds) {
            return alignment switch {
                HorizontalAlignment.Left => 0,
                HorizontalAlignment.Center => bounds.Width / 2f,
                HorizontalAlignment.Right => bounds.Width,
                _ => throw new InvalidOperationException($"Unknown horizontal alignment: {alignment}")
            };
        }
    }
}
