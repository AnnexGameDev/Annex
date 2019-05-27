namespace Annex.Data
{
    public class Vector2f
    {
        public virtual float X { get; set; }
        public virtual float Y { get; set; }

        public Vector2f() {

        }

        public Vector2f(float x, float y) {
            this.X = x;
            this.Y = y;
        }

        public static implicit operator SFML.System.Vector2f(Vector2f source) {
            return new SFML.System.Vector2f(source.X, source.Y);
        }

        public void Set(float x, float y) {
            this.X = x;
            this.Y = y;
        }
    }
}
