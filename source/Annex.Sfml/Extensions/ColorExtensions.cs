using Annex.Core.Data;
using SFML.Graphics;

namespace Annex.Sfml.Extensions
{
    public static class ColorExtensions
    {
        public static bool DoesNotEqual(this Color sfmlColor, RGBA annexColor) {
            return sfmlColor.A != annexColor.A
                || sfmlColor.R != annexColor.R
                || sfmlColor.G != annexColor.G
                || sfmlColor.B != annexColor.B;
        }

        public static bool DoesNotEqual(this Color sfmlColor, RGBA? annexColor, Color defaultColor) {
            if (annexColor == null) {
                if (sfmlColor == defaultColor)
                    return false;
                return true;
            }

            return DoesNotEqual(sfmlColor, annexColor);
        }

        public static Color ToSFML(this RGBA? color, RGBA defaultColor) {
            if (color == null)
                return new Color(defaultColor.R, defaultColor.G, defaultColor.B, defaultColor.A);
            return new Color(color.R, color.G, color.B, color.A);
        }

        public static Color ToSFML(this RGBA color) {
            return new Color(color.R, color.G, color.B, color.A);
        }
    }
}
