using Annex.Core.Data;

namespace Annex.Sfml.Extensions;

public static class VectorExtensions
{
    public static void Set(this SFML.System.Vector2i vector, int x, int y)
    {
        vector.X = x;
        vector.Y = y;
    }

    public static void Set(this SFML.System.Vector2u vector, uint x, uint y)
    {
        vector.X = x;
        vector.Y = y;
    }

    public static void Set(this SFML.System.Vector2i vector, IReadonlyVector2<float> value)
    {
        vector.X = (int)value.X;
        vector.Y = (int)value.Y;
    }

    public static void Set(this SFML.System.Vector2u vector, IReadonlyVector2<float> value)
    {
        vector.X = (uint)value.X;
        vector.Y = (uint)value.Y;
    }

    public static void Set(this SFML.System.Vector2u vector, IReadonlyVector2<uint> value)
    {
        vector.X = value.X;
        vector.Y = value.Y;
    }

    public static bool DoesNotEqual(this SFML.System.Vector2f vector, IReadonlyVector2<float>? value)
    {
        return vector.DoesNotEqual(value, 0, 0);
    }

    public static bool DoesNotEqual(this SFML.System.Vector2f vector, IReadonlyVector2<float>? value, float defaultX, float defaultY)
    {
        if (value == null)
        {
            if (vector.X == defaultX && vector.Y == defaultY)
                return false;
            return true;
        }
        return vector.X != value.X || vector.Y != value.Y;
    }

    public static SFML.System.Vector2f ToSFML(this IReadonlyVector2<float>? value, float defaultX = 0, float defaultY = 0)
    {
        return new SFML.System.Vector2f(value?.X ?? defaultX, value?.Y ?? defaultY);
    }
}