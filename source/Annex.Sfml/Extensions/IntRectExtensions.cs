using SFML.Graphics;

namespace Annex.Sfml.Extensions
{
    public static class IntRectExtensions
    {
        public static IntRect ToSFML(this Core.Data.IntRect? rect) {
            if (rect == null)
                return new IntRect();
            return new IntRect(rect.Left, rect.Top, rect.Width, rect.Height);
        }

        public static bool DoesNotEqual(this IntRect sfmlRect, Core.Data.IntRect? annexRect, IntRect defaultRect) {
            if (annexRect == null) {
                if (sfmlRect == defaultRect)
                    return false;
                return true;
            }

            return sfmlRect.Width != annexRect.Width || sfmlRect.Height != annexRect.Height
                || sfmlRect.Top != annexRect.Top || sfmlRect.Left != annexRect.Left;
        }
    }
}
