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

        public static bool DoesNotEqual(this IntRect sfmlRect, Core.Data.IntRect? annexRect, int top, int left, int width, int height) {
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
