namespace Annex.Core.Data;

public interface IShared<T>
{
    public T Value { get; set; }

    void Set(T value);
}

public class Shared<T> : IShared<T>
{
    public T Value { get; set; }

    public Shared(T value) {
        Value = value;
    }

    public void Set(T value) {
        this.Value = value;
    }

    public static implicit operator Shared<T>(T value) {
        return new Shared<T>(value);
    }

    public static explicit operator T(Shared<T> shared) {
        return shared.Value;
    }
}

public static class SharedExtensions
{
    public static IShared<T> ToShared<T>(this T value) {
        // TODO: Wait for C# 11 to use static abstract implicit/explicit operator
        return new Shared<T>(value);
    }
}
