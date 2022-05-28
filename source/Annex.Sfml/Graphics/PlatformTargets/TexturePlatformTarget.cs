using Annex_Old.Core.Graphics.Contexts;
using Annex_Old.Sfml.Collections.Generic;

namespace Annex_Old.Sfml.Graphics.PlatformTargets
{
    internal class TexturePlatformTarget : SpritePlatformTarget
    {
        private readonly TextureContext _textureContext;

        public TexturePlatformTarget(TextureContext context, ITextureCache textureCache) : base(textureCache) {
            this._textureContext = context;
        }

        protected override void UpdateIfNeeded() {

            if (string.IsNullOrEmpty(this._textureContext.TextureId.Value)) {
                return;
            }

            var texture = UpdateTexture(this._textureContext.TextureId.Value);
            var rect = UpdateTextureRect(this._textureContext.SourceTextureRect);

            // Compute scale
            int textureWidth = (int)texture.Size.X;
            int textureHeight = (int)texture.Size.Y;
            float sourceX = GetSourceWidth(textureWidth);
            float sourceY = GetSourceHeight(textureHeight);
            float desiredRenderX = GetDesiredRenderX(textureWidth);
            float desiredRenderY = GetDesiredRenderY(textureHeight);

            float scaleX = desiredRenderX / sourceX;
            float scaleY = desiredRenderY / sourceY;
            var scale = UpdateScale(scaleX, scaleY);

            (var position, var origin) = UpdatePositionAndOrigin(this._textureContext.Position, this._textureContext.RenderOffset);
            var color = UpdateColor(this._textureContext.RenderColor);
            var rotation = UpdateRotation(this._textureContext.Rotation);
        }

        private float GetDesiredRenderX(int textureX) {
            float sourceWidth = this._textureContext.SourceTextureRect?.Width ?? textureX;
            return this._textureContext.RenderSize?.X ?? sourceWidth;
        }

        private float GetDesiredRenderY(int textureY) {
            float sourceHeight = this._textureContext.SourceTextureRect?.Height ?? textureY;
            return this._textureContext.RenderSize?.Y ?? sourceHeight;
        }

        private float GetSourceWidth(int textureX) {
            return this._textureContext.SourceTextureRect?.Width ?? textureX;
        }

        private float GetSourceHeight(int textureY) {
            return this._textureContext.SourceTextureRect?.Height ?? textureY;
        }
    }
}
