using Annex.Core.Data;
using Annex.Sfml.Collections.Generic;
using Annex.Sfml.Extensions;
using SFML.Graphics;
using IntRect = SFML.Graphics.IntRect;

namespace Annex.Sfml.Graphics.PlatformTargets
{
    internal abstract class SpritePlatformTarget : TransformablePlatformTarget
    {
        private readonly Sprite _sprite;
        protected override Transformable Transformable => this._sprite;
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
    }
}
