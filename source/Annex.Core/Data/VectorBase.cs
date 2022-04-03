using System.Diagnostics;

namespace Annex.Core.Data
{
    [DebuggerDisplay("X:{X} Y:{Y}")]
    public class VectorBase<T> : IVector2<T> where T : struct
    {
        private T _x;
        public T X
        {
            get => this._x;
            set => this._x = value;
        }

        private T _y;
        public T Y
        {
            get => this._y;
            set => this._y = value;
        }

        public VectorBase(T x, T y) {
            this.Set(x, y);
        }

        public void Set(T x, T y) {
            this._x = x;
            this._y = y;
        }
    }
}