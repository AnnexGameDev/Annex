namespace Annex.Core.Data;

public interface IVector2<T> : IReadonlyVector2<T>
{
    void Set(IVector2<T> vector);
    void Set(T x, T y);
}
