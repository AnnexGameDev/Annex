namespace Annex.Data.Shared
{
    public class Vector
    {
        public virtual float X { get; set; }
        public virtual float Y { get; set; }

        public Vector() {

        }

        public Vector(float x, float y) {
            this.X = x;
            this.Y = y;
        }

        public static implicit operator SFML.System.Vector2f(Vector source) {
            return new SFML.System.Vector2f(source.X, source.Y);
        }

        public void Set(float x, float y) {
            this.X = x;
            this.Y = y;
        }

        public void Add(Vector vector) {
            this.X += vector.X;
            this.Y += vector.Y;
        }

        public void Add(float x, float y) {
            this.X += x;
            this.Y += y;
        }
    }
}
