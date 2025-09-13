using Annex.Core.Graphics.Contexts;
using Annex.Sfml.Collections.Generic;

namespace Annex.Sfml.Graphics.PlatformTargets;

internal class SpritesheetPlatformTarget : SpritePlatformTarget
{
    private SpritesheetContext _spritesheetContext;

    public SpritesheetPlatformTarget(SpritesheetContext spritesheetContext, ITextureCache textureCache) : base(textureCache)
    {
        _spritesheetContext = spritesheetContext;
    }

    protected override void UpdateIfNeeded()
    {
        if (string.IsNullOrEmpty(_spritesheetContext.TextureId.Value))
        {
            return;
        }

        var texture = UpdateTexture(_spritesheetContext.TextureId.Value);

        int frameSizeX = (int)texture.Size.X / _spritesheetContext.NumColumns;
        int frameSizeY = (int)texture.Size.Y / _spritesheetContext.NumRows;
        int top = _spritesheetContext.Row * frameSizeY;
        int left = _spritesheetContext.Column * frameSizeX;
        var rect = UpdateTextureRect(top, left, frameSizeX, frameSizeY);

        float desiredRenderX = _spritesheetContext.RenderSize?.X ?? frameSizeX;
        float desiredRenderY = _spritesheetContext.RenderSize?.Y ?? frameSizeY;

        float scaleX = desiredRenderX / frameSizeX;
        float scaleY = desiredRenderY / frameSizeY;
        var scale = UpdateScale(scaleX, scaleY);

        (var position, var origin) = UpdatePositionAndOrigin(_spritesheetContext.Position, _spritesheetContext.RenderOffset);
        var color = UpdateColor(_spritesheetContext.RenderColor);
        var rotation = UpdateRotation(_spritesheetContext.Rotation);
    }
}
