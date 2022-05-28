namespace Annex_Old.Core.Data
{
    public interface IVector2<T>
    {
        T X { get; }
        T Y { get; }

        void Set(IVector2<T> vector);
        void Set(T x, T y);
    }
}
