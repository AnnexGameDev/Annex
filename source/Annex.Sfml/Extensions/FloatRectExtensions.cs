using SFML.Graphics;

namespace Annex.Sfml.Extensions
{
    internal static class FloatRectExtensions
    {
        public static FloatRect ToSFML(this Core.Data.FloatRect? rect) {
            if (rect == null)
                return new FloatRect();
            return new FloatRect(rect.Left, rect.Top, rect.Height, rect.Height);
        }

        public static bool DoesNotEqual(this FloatRect sfmlRect, Core.Data.FloatRect? annexRect, int top = 0, int left = 0, int width = 0, int height = 0) {
            if (annexRect == null) {
                if (sfmlRect.Top == top
                    && sfmlRect.Left == left
                    && sfmlRect.Width == width
                    && sfmlRect.Height == height)
                    return false;
                return true;
            }

            return sfmlRect.Width != annexRect.Width || sfmlRect.Height != annexRect.Height
                || sfmlRect.Top != annexRect.Top || sfmlRect.Left != annexRect.Left;
        }
    }
}
