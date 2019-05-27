namespace Annex.Data.ReferenceTypes
{
    public class PVector
    {
        public virtual float X { get; set; }
        public virtual float Y { get; set; }

        public PVector() {

        }

        public PVector(float x, float y) {
            this.X = x;
            this.Y = y;
        }

        public static implicit operator SFML.System.Vector2f(PVector source) {
            return new SFML.System.Vector2f(source.X, source.Y);
        }

        public void Set(float x, float y) {
            this.X = x;
            this.Y = y;
        }
    }
}
