namespace Annex.Core.Data;

public class OffsetVector2f : IVector2<float>
{
    public IReadonlyVector2<float> BaseVector { get; }
    public IReadonlyVector2<float> OffsetVector { get; private set; }

    public float X => BaseVector.X + OffsetVector.X;
    public float Y => BaseVector.Y + OffsetVector.Y;

    public OffsetVector2f(IReadonlyVector2<float> baseVector, float xOffset, float yOffset) : this(baseVector, new Vector2f(xOffset, yOffset))
    {

    }

    public OffsetVector2f(IReadonlyVector2<float> baseVector, IReadonlyVector2<float> offsetVector)
    {
        BaseVector = baseVector;
        OffsetVector = offsetVector;
    }

    public void Set(IVector2<float> vector)
    {
        throw new NotImplementedException($"{nameof(OffsetVector2f)} doesn't support {nameof(Set)}");
    }

    public void Set(float x, float y)
    {
        throw new NotImplementedException($"{nameof(OffsetVector2f)} doesn't support {nameof(Set)}");
    }
}
