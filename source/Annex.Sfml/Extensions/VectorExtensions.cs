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
    }
}