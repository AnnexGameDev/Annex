namespace Annex.Core.Data
{
    public class Vector2i : VectorBase<int>
    {
        public Vector2i(Action? onChangeCallback = null) : this(0, 0, onChangeCallback) {
        }

        public Vector2i(int x, int y, Action? onChangeCallback = null) : base(x, y, onChangeCallback) {
        }
    }
}