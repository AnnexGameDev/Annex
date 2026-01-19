using Annex.Core.Assets;
using Annex.Core.Graphics.Contexts;

namespace Annex.Sfml.Graphics.PlatformTargets;

internal class SpritesheetPlatformTargetCreator : PlatformTargetCreator<SpritesheetPlatformTarget>
{
    protected override PlatformTarget CreatePlatformTargetFor(DrawContext drawContext, AssetRegistry assets)
    {
        var textures = assets.GetTextures();
        return new SpritesheetPlatformTarget((SpritesheetContext)drawContext, textures);
    }

    protected override bool Supports(DrawContext drawContext)
    {
        return drawContext is SpritesheetContext;
    }
}
