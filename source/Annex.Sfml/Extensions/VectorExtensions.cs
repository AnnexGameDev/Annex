using Annex.Core.Data;

namespace Annex.Sfml.Extensions
{
    public static class VectorExtensions
    {
        public static void Set(this SFML.System.Vector2i vector, Vector2i value) {
            vector.X = value.X;
            vector.Y = value.Y;
        }

        public static void Set(this SFML.System.Vector2u vector, Vector2ui value) {
            vector.X = value.X;
            vector.Y = value.Y;
        }

        public static bool DoesNotEqual(this SFML.System.Vector2f vector, IVector2<float>? value) {
            return vector.DoesNotEqual(value, 0, 0);
        }
        
        public static bool DoesNotEqual(this SFML.System.Vector2f vector, IVector2<float>? value, float defaultX, float defaultY) {
            if (value == null) {
                if (vector.X == defaultX && vector.Y == defaultY)
                    return false;
                return true;
            }
            return vector.X != value.X || vector.Y != value.Y;
        }

        public static SFML.System.Vector2f ToSFML(this IVector2<float>? value, float defaultX = 0, float defaultY = 0) {
            return new SFML.System.Vector2f(value?.X ?? defaultX, value?.Y ?? defaultY);
        }
    }
}