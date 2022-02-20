namespace Annex.Core.Data
{
    public class Vector2f : VectorBase<float>
    {
        public Vector2f(Action? onChangeCallback = null) : this(0, 0, onChangeCallback) {
        }

        public Vector2f(float x, float y, Action? onChangeCallback = null) : base(x, y, onChangeCallback) {
        }
    }
}