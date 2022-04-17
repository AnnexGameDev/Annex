namespace Annex.Core.Data
{
    public class Vector2f : VectorBase<float>
    {
        public Vector2f() : this(0, 0) {
        }

        public Vector2f(float x, float y) : base(x, y) {
        }

        public void Scale(float scale) {
            this.X *= scale;
            this.Y *= scale;
        }

        public void Add(float dx, float dy) {
            this.X += dx;
            this.Y += dy;
        }

        public void Add(IVector2<float> vector) {
            this.Add(vector.X, vector.Y);
        }

        public void Set(IVector2<float> vector) {
            this.Set(vector.X, vector.Y);
        }

        public static Vector2f SumOf(params IVector2<float>[] vectors) {
            var sum = new Vector2f();

            foreach (var v in vectors) {
                sum.Add(v);
            }

            return sum;
        }
    }
}