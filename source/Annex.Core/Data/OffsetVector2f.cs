namespace Annex.Core.Data;

public class OffsetVector2f : IVector2<float>
{
    public IVector2<float> BaseVector { get; }
    public IVector2<float> OffsetVector { get; private set; }

    public float X => BaseVector.X + OffsetVector.X;
    public float Y => BaseVector.Y + OffsetVector.Y;

    public OffsetVector2f(IVector2<float> baseVector, float xOffset, float yOffset) : this(baseVector, new Vector2f(xOffset, yOffset))
    {

    }

    public OffsetVector2f(IVector2<float> baseVector, IVector2<float> offsetVector)
    {
        BaseVector = baseVector;
        OffsetVector = offsetVector;
    }

    public void Set(IVector2<float> vector)
    {
        OffsetVector = vector;
    }

    public void Set(float x, float y)
    {
        OffsetVector.Set(x, y);
    }
}
