namespace Annex.Sfml.Extensions
{
    public static class VectorExtensions
    {
        public static void Set(this SFML.System.Vector2i vector, Core.Data.Vector2i value) {
            vector.X = value.X;
            vector.Y = value.Y;
        }

        public static void Set(this SFML.System.Vector2u vector, Core.Data.Vector2ui value) {
            vector.X = value.X;
            vector.Y = value.Y;
        }

        public static bool DoesNotEqual(this SFML.System.Vector2f vector, Core.Data.Vector2f? value) {
            if (value == null)
                return true;
            return vector.X != value.X || vector.Y != value.Y;
        }

        public static SFML.System.Vector2f ToSFML(this Core.Data.Vector2f? value, float defaultX = 0, float defaultY = 0) {
            return new SFML.System.Vector2f(value?.X ?? defaultX, value?.Y ?? defaultY);
        }
    }
}