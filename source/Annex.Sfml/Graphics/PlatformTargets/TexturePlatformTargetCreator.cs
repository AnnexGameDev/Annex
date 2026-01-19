using Annex.Core.Assets;
using Annex.Core.Graphics.Contexts;

namespace Annex.Sfml.Graphics.PlatformTargets;

internal class TexturePlatformTargetCreator : PlatformTargetCreator<TexturePlatformTarget>
{
    protected override PlatformTarget CreatePlatformTargetFor(DrawContext drawContext, AssetRegistry assets)
    {
        var textures = assets.GetTextures();
        return new TexturePlatformTarget((TextureContext)drawContext, textures);
    }

    protected override bool Supports(DrawContext drawContext)
    {
        return drawContext is TextureContext;
    }
}
