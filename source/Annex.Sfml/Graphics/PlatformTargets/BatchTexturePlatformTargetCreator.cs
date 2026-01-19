using Annex.Core.Assets;
using Annex.Core.Graphics.Contexts;

namespace Annex.Sfml.Graphics.PlatformTargets;

internal class BatchTexturePlatformTargetCreator : PlatformTargetCreator<BatchTexturePlatformTarget>
{
    protected override PlatformTarget CreatePlatformTargetFor(DrawContext drawContext, AssetRegistry assets)
    {
        var textures = assets.GetTextures();
        return new BatchTexturePlatformTarget((BatchTextureContext)drawContext, textures);
    }

    protected override bool Supports(DrawContext drawContext)
    {
        return drawContext is BatchTextureContext;
    }
}
