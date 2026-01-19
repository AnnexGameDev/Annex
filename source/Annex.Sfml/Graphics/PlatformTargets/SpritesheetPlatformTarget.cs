using Annex.Core.Assets;
using Annex.Core.Graphics.Contexts;

namespace Annex.Sfml.Graphics.PlatformTargets;

internal class SpritesheetPlatformTarget : SpritePlatformTarget<SpritesheetContext>
{
    public SpritesheetPlatformTarget(SpritesheetContext spritesheetContext, IAssetStore textures) : base(spritesheetContext, textures)
    {
    }

    protected override void UpdateIfNeeded()
    {
        if (string.IsNullOrEmpty(Context.TextureId.Value))
        {
            return;
        }

        var texture = UpdateTexture(Context.TextureId.Value);

        int frameSizeX = (int)texture.Size.X / Context.NumColumns;
        int frameSizeY = (int)texture.Size.Y / Context.NumRows;
        int top = Context.Row * frameSizeY;
        int left = Context.Column * frameSizeX;
        var rect = UpdateTextureRect(top, left, frameSizeX, frameSizeY);

        float desiredRenderX = Context.RenderSize?.X ?? frameSizeX;
        float desiredRenderY = Context.RenderSize?.Y ?? frameSizeY;

        float scaleX = desiredRenderX / frameSizeX;
        float scaleY = desiredRenderY / frameSizeY;
        var scale = UpdateScale(scaleX, scaleY);

        (var position, var origin) = UpdatePositionAndOrigin(Context.Position, Context.RenderOffset);
        var color = UpdateColor(Context.RenderColor);
        var rotation = UpdateRotation(Context.Rotation);
    }
}
