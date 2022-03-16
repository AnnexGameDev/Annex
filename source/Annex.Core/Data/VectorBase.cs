using System.Diagnostics;

namespace Annex.Core.Data
{
    [DebuggerDisplay("X:{X} Y:{Y}")]
    public class VectorBase<T> : IVector2<T> where T : struct
    {
        // TODO: Get rid of this.
        private Action? _onChangeCallback;

        private T _x;
        public T X
        {
            get => this._x;
            set
            {
                this._x = value;
                this._onChangeCallback?.Invoke();
            }
        }

        private T _y;
        public T Y
        {
            get => this._y;
            set
            {
                this._y = value;
                this._onChangeCallback?.Invoke();
            }
        }

        public VectorBase(T x, T y, Action? onChangeCallback) {
            this._onChangeCallback = onChangeCallback;
            this.Set(x, y);
        }

        public void Set(T x, T y) {
            this._x = x;
            this._y = y;
            this._onChangeCallback?.Invoke();
        }
    }
}