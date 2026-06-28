namespace Annex.Core.Data;

public class Vector2f : VectorBase<float>
{
    public Vector2f() : this(0, 0)
    {
    }

    public Vector2f(float x, float y) : base(x, y)
    {
    }

    public void Scale(float scale)
    {
        X *= scale;
        Y *= scale;
    }

    public void Add(float dx, float dy)
    {
        X += dx;
        Y += dy;
    }

    public void Add(IVector2<float> vector)
    {
        Add(vector.X, vector.Y);
    }

    public void Set(IReadonlyVector2<float> vector)
    {
        Set(vector.X, vector.Y);
    }

    public static Vector2f SumOf(params IVector2<float>[] vectors)
    {
        var sum = new Vector2f();

        foreach (var v in vectors)
        {
            sum.Add(v);
        }

        return sum;
    }

    public static IVector2<float> CenterInside(float containerX, float containerY, float elementToCenterX, float elementToCenterY)
    {
        return new Vector2f((containerX - elementToCenterX) / 2, (containerY - elementToCenterY) / 2);
    }

    public static IVector2<float> CenterInside(IVector2<float> container, IVector2<float> elementToCenter)
    {
        return CenterInside(container.X, container.Y, elementToCenter.X, elementToCenter.Y);
    }

    public static IVector2<float> CenterInside(IVector2<float> container, float elementToCenterX, float elementToCenterY)
    {
        return CenterInside(container.X, container.Y, elementToCenterX, elementToCenterY);
    }
}