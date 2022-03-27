using Annex.Core.Data;
using Annex.Sfml.Collections.Generic;
using Annex.Sfml.Extensions;
using SFML.Graphics;
using IntRect = SFML.Graphics.IntRect;
using Vector2f = SFML.System.Vector2f;

namespace Annex.Sfml.Graphics.PlatformTargets
{
    internal abstract class SpritePlatformTarget : PlatformTarget
    {
        private readonly Sprite _sprite;
        private readonly ITextureCache _textureCache;

        public SpritePlatformTarget(ITextureCache textureCache) {
            this._textureCache = textureCache;
            this._sprite = new();
        }

        public override void Dispose() {
            this._sprite.Dispose();
        }

        protected override void Draw(RenderTarget renderTarget) {
            this.UpdateIfNeeded();
            renderTarget.Draw(this._sprite);
        }

        protected abstract void UpdateIfNeeded();

        protected Texture UpdateTexture(string textureId) {
            var texture = this._textureCache.GetTexture(textureId);
            if (texture != this._sprite.Texture) {
                this._sprite.Texture = texture;
            }
            return this._sprite.Texture;
        }

        protected IntRect UpdateTextureRect(Core.Data.IntRect? sourceTextureRect) {
            if (this._sprite.TextureRect.DoesNotEqual(sourceTextureRect, 0, 0, (int)this._sprite.Texture.Size.X, (int)this._sprite.Texture.Size.Y)) {
                this._sprite.TextureRect = sourceTextureRect.ToSFML();
            }
            return this._sprite.TextureRect;
        }

        protected IntRect UpdateTextureRect(int top, int left, int width, int height) {
            if (this._sprite.TextureRect.DoesNotEqual(null, top, left, width, height)) {
                this._sprite.TextureRect = new IntRect(top, left, width, height);
            }
            return this._sprite.TextureRect;
        }

        protected Color UpdateColor(RGBA? color) {
            if (this._sprite.Color.DoesNotEqual(color, Color.White)) {
                this._sprite.Color = color.ToSFML(KnownColor.White);
            }
            return this._sprite.Color;
        }

        protected float UpdateRotation(Shared<float>? rotation) {
            float trueRotation = rotation?.Value ?? 0;
            if (this._sprite.Rotation != trueRotation) {
                this._sprite.Rotation = trueRotation;
            }
            return this._sprite.Rotation;
        }

        protected Vector2f UpdateScale(float scaleX, float scaleY) {
            var scale = new Vector2f(scaleX, scaleY);
            if (this._sprite.Scale != scale) {
                this._sprite.Scale = scale;
            }
            return this._sprite.Scale;
        }

        protected Vector2f UpdatePosition(float x, float y) {
            var position = new Vector2f(x, y);
            if (this._sprite.Position != position) {
                this._sprite.Position = position;
            }
            return this._sprite.Position;
        }

        protected Vector2f UpdateOrigin(float x, float y) {
            var origin = new Vector2f(x, y);
            if (this._sprite.Origin != origin) {
                this._sprite.Origin = origin;
            }
            return this._sprite.Origin;
        }

        protected (Vector2f position, Vector2f origin) UpdatePositionAndOrigin(IVector2<float> position, IVector2<float>? renderOffset) {
            var positionX = position.X + (renderOffset?.X ?? 0) * this._sprite.TextureRect.Width;
            var positionY = position.Y + (renderOffset?.Y ?? 0) * this._sprite.TextureRect.Height;
            var finalPosition = UpdatePosition(positionX, positionY);

            var originX = (position.X - positionX);
            var originY = (position.Y - positionY);
            var finalOrigin = UpdateOrigin(originX, originY);

            return (finalPosition, finalOrigin);
        }
    }
}
