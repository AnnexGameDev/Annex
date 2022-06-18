using Annex.Core.Graphics.Contexts;
using Annex.Sfml.Collections.Generic;

namespace Annex.Sfml.Graphics.PlatformTargets
{
    internal class SpritesheetPlatformTarget : SpritePlatformTarget
    {
        private SpritesheetContext _spritesheetContext;

        public SpritesheetPlatformTarget(SpritesheetContext spritesheetContext, ITextureCache textureCache) : base(textureCache) {
            this._spritesheetContext = spritesheetContext;
        }

        protected override void UpdateIfNeeded() {
            if (string.IsNullOrEmpty(this._spritesheetContext.TextureId.Value)) {
                return;
            }

            var texture = this.UpdateTexture(this._spritesheetContext.TextureId.Value);

            int frameSizeX = (int)texture.Size.X / this._spritesheetContext.NumColumns;
            int frameSizeY = (int)texture.Size.Y / this._spritesheetContext.NumRows;
            int top = this._spritesheetContext.Row * frameSizeY;
            int left = this._spritesheetContext.Column * frameSizeX;
            var rect = UpdateTextureRect(top, left, frameSizeX, frameSizeY);

            float desiredRenderX = this._spritesheetContext.RenderSize?.X ?? frameSizeX;
            float desiredRenderY = this._spritesheetContext.RenderSize?.Y ?? frameSizeY;

            float scaleX = desiredRenderX / frameSizeX;
            float scaleY = desiredRenderY / frameSizeY;
            var scale = UpdateScale(scaleX, scaleY);

            (var position, var origin) = UpdatePositionAndOrigin(this._spritesheetContext.Position, this._spritesheetContext.RenderOffset);
            var color = UpdateColor(this._spritesheetContext.RenderColor);
            var rotation = UpdateRotation(this._spritesheetContext.Rotation);
        }
    }
}
