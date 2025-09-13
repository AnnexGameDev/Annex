using Annex.Core.Graphics.Contexts;
using Annex.Sfml.Collections.Generic;

namespace Annex.Sfml.Graphics.PlatformTargets;

internal class TexturePlatformTarget : SpritePlatformTarget
{
    private readonly TextureContext _textureContext;

    public TexturePlatformTarget(TextureContext context, ITextureCache textureCache) : base(textureCache)
    {
        _textureContext = context;
    }

    protected override void UpdateIfNeeded()
    {

        if (string.IsNullOrEmpty(_textureContext.TextureId.Value))
        {
            return;
        }

        var texture = UpdateTexture(_textureContext.TextureId.Value);
        var rect = UpdateTextureRect(_textureContext.SourceTextureRect);

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

        (var position, var origin) = UpdatePositionAndOrigin(_textureContext.Position, _textureContext.RenderOffset);
        var color = UpdateColor(_textureContext.RenderColor);
        var rotation = UpdateRotation(_textureContext.Rotation);
    }

    private float GetDesiredRenderX(int textureX)
    {
        float sourceWidth = _textureContext.SourceTextureRect?.Width ?? textureX;
        return _textureContext.RenderSize?.X ?? sourceWidth;
    }

    private float GetDesiredRenderY(int textureY)
    {
        float sourceHeight = _textureContext.SourceTextureRect?.Height ?? textureY;
        return _textureContext.RenderSize?.Y ?? sourceHeight;
    }

    private float GetSourceWidth(int textureX)
    {
        return _textureContext.SourceTextureRect?.Width ?? textureX;
    }

    private float GetSourceHeight(int textureY)
    {
        return _textureContext.SourceTextureRect?.Height ?? textureY;
    }
}
