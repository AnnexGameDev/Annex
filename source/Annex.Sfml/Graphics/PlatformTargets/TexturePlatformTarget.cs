using Annex.Core.Assets;
using Annex.Core.Graphics.Contexts;

namespace Annex.Sfml.Graphics.PlatformTargets;

internal class TexturePlatformTarget : SpritePlatformTarget<TextureContext>
{
    public TexturePlatformTarget(TextureContext context, IAssetStore textures) : base(context, textures)
    {
    }

    protected override void UpdateIfNeeded()
    {
        if (string.IsNullOrEmpty(Context.TextureId))
        {
            return;
        }

        var texture = UpdateTexture(Context.TextureId);
        var rect = UpdateTextureRect(Context.SourceTextureRect);

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

        (var position, var origin) = UpdatePositionAndOrigin(Context.Position, Context.RenderOffset);
        var color = UpdateColor(Context.RenderColor);
        var rotation = UpdateRotation(Context.Rotation);
    }

    private float GetDesiredRenderX(int textureX)
    {
        float sourceWidth = Context.SourceTextureRect?.Width ?? textureX;
        return Context.RenderSize?.X ?? sourceWidth;
    }

    private float GetDesiredRenderY(int textureY)
    {
        float sourceHeight = Context.SourceTextureRect?.Height ?? textureY;
        return Context.RenderSize?.Y ?? sourceHeight;
    }

    private float GetSourceWidth(int textureX)
    {
        return Context.SourceTextureRect?.Width ?? textureX;
    }

    private float GetSourceHeight(int textureY)
    {
        return Context.SourceTextureRect?.Height ?? textureY;
    }
}
