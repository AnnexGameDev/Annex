namespace Annex.Core.Data
{
    public class Vector2ui : VectorBase<uint>
    {
        public Vector2ui(Action? onChangeCallback = null) : this(0, 0, onChangeCallback) {
        } 

        public Vector2ui(uint x, uint y, Action? onChangeCallback = null) : base(x, y, onChangeCallback) {
        }
    }
}