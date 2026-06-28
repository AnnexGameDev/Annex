namespace Annex.Core.Data;

public readonly struct Position(float x, float y)
{
    public float X { get; } = x;
    public float Y { get; } = y;
}
