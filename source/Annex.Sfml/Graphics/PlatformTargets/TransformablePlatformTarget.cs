using Annex_Old.Core.Data;
using SFML.Graphics;
using Vector2f = SFML.System.Vector2f;

namespace Annex_Old.Sfml.Graphics.PlatformTargets
{
    internal abstract class TransformablePlatformTarget : PlatformTarget
    {
        protected abstract Transformable Transformable { get; }

        protected float UpdateRotation(Shared<float>? rotation) {
            float trueRotation = rotation?.Value ?? 0;
            if (this.Transformable.Rotation != trueRotation) {
                this.Transformable.Rotation = trueRotation;
            }
            return this.Transformable.Rotation;
        }

        protected Vector2f UpdateScale(float scaleX, float scaleY) {
            var scale = new Vector2f(scaleX, scaleY);
            if (this.Transformable.Scale != scale) {
                this.Transformable.Scale = scale;
            }
            return this.Transformable.Scale;
        }

        protected Vector2f UpdatePosition(float x, float y) {
            var position = new Vector2f(x, y);
            if (this.Transformable.Position != position) {
                this.Transformable.Position = position;
            }
            return this.Transformable.Position;
        }

        protected Vector2f UpdateOrigin(float x, float y) {
            var origin = new Vector2f(x, y);
            if (this.Transformable.Origin != origin) {
                this.Transformable.Origin = origin;
            }
            return this.Transformable.Origin;
        }

        protected (Vector2f position, Vector2f origin) UpdatePositionAndOrigin(IVector2<float> position, IVector2<float>? renderOffset) {
            var finalPosition = UpdatePosition(position.X, position.Y);

            var offsetPositionX = position.X + (renderOffset?.X ?? 0) / this.Transformable.Scale.X;
            var offsetPositionY = position.Y + (renderOffset?.Y ?? 0) / this.Transformable.Scale.Y;
            var originX = (position.X - offsetPositionX);
            var originY = (position.Y - offsetPositionY);
            var finalOrigin = UpdateOrigin(originX, originY);

            return (finalPosition, finalOrigin);
        }
    }
}
