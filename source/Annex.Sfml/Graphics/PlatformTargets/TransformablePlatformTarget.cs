using Annex.Core.Data;
using SFML.Graphics;
using Vector2f = SFML.System.Vector2f;

namespace Annex.Sfml.Graphics.PlatformTargets;

internal abstract class TransformablePlatformTarget : PlatformTarget
{
    protected abstract Transformable Transformable { get; }

    protected float UpdateRotation(float? rotation)
    {
        float trueRotation = rotation ?? 0;
        if (Transformable.Rotation != trueRotation)
        {
            Transformable.Rotation = trueRotation;
        }
        return Transformable.Rotation;
    }

    protected Vector2f UpdateScale(float scaleX, float scaleY)
    {
        var scale = new Vector2f(scaleX, scaleY);
        if (Transformable.Scale != scale)
        {
            Transformable.Scale = scale;
        }
        return Transformable.Scale;
    }

    protected Vector2f UpdatePosition(float x, float y)
    {
        var position = new Vector2f(x, y);
        if (Transformable.Position != position)
        {
            Transformable.Position = position;
        }
        return Transformable.Position;
    }

    protected Vector2f UpdateOrigin(float x, float y)
    {
        var origin = new Vector2f(x, y);
        if (Transformable.Origin != origin)
        {
            Transformable.Origin = origin;
        }
        return Transformable.Origin;
    }

    protected (Vector2f position, Vector2f origin) UpdatePositionAndOrigin(IReadonlyVector2<float> position, IReadonlyVector2<float>? renderOffset)
    {
        var finalPosition = UpdatePosition(position.X, position.Y);

        var offsetPositionX = position.X + (renderOffset?.X ?? 0) / Transformable.Scale.X;
        var offsetPositionY = position.Y + (renderOffset?.Y ?? 0) / Transformable.Scale.Y;
        var originX = (position.X - offsetPositionX);
        var originY = (position.Y - offsetPositionY);
        var finalOrigin = UpdateOrigin(originX, originY);

        return (finalPosition, finalOrigin);
    }
}
