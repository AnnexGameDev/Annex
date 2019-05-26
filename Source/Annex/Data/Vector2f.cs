namespace Annex.Data
{
    public class Vector2f
    {
        public float X { get; set; }
        public float Y { get; set; }

        public Vector2f() {
            this.X = 0;
            this.Y = 0;
        }

        public Vector2f(float x, float y) {
            this.X = x;
            this.Y = y;
        }

        public static implicit operator SFML.System.Vector2f(Vector2f source) {
            return new SFML.System.Vector2f(source.X, source.Y);
        }
    }
}
