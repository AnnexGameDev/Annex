namespace Annex.Sfml.Extensions
{
    public static class VectorExtensions
    {
        public static void Set(this SFML.System.Vector2i vector, SFML.System.Vector2i value) {
            vector.X = value.X;
            vector.Y = value.Y;
        }
        public static Core.Data.Vector2i ToAnnexVector(this SFML.System.Vector2i sfmlVector) {
            return new Core.Data.Vector2i(sfmlVector.X, sfmlVector.Y);
        }

        public static SFML.System.Vector2i ToSfmlVector(this Core.Data.Vector2i annexVector) {
            return new SFML.System.Vector2i(annexVector.X, annexVector.Y);
        }

        public static void Set(this SFML.System.Vector2u vector, SFML.System.Vector2u value) {
            vector.X = value.X;
            vector.Y = value.Y;
        }

        public static Core.Data.Vector2ui ToAnnexVector(this SFML.System.Vector2u sfmlVector) {
            return new Core.Data.Vector2ui(sfmlVector.X, sfmlVector.Y);
        }

        public static SFML.System.Vector2u ToSfmlVector(this Core.Data.Vector2ui annexVector) {
            return new SFML.System.Vector2u(annexVector.X, annexVector.Y);
        }
    }
}