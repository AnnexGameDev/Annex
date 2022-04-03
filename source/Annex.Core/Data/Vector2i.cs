namespace Annex.Core.Data
{
    public class Vector2i : VectorBase<int>
    {
        public Vector2i() : this(0, 0) {
        }

        public Vector2i(int x, int y) : base(x, y) {
        }
    }
}