using SFML.Graphics;

namespace Annex.Sfml.Extensions
{
    public static class IntRectExtensions
    {
        public static IntRect ToSFML(this Core.Data.IntRect? rect, int defaultValue = 0) {
            if (rect == null)
                return new IntRect();
            return new IntRect(rect.Left, rect.Top, rect.Width, rect.Height);
        }

        public static bool DoesNotEqual(this IntRect annexRect, Core.Data.IntRect? sfmlRect) {
            if (sfmlRect == null)
                return true;

            return annexRect.Width != sfmlRect.Width || annexRect.Height != sfmlRect.Height
                || annexRect.Top != sfmlRect.Top || annexRect.Left != sfmlRect.Left;
        }
    }
}
