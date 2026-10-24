using Annex.Core.Assets;
using Annex.Core.Graphics.Contexts;

namespace Annex.Sfml.Graphics.PlatformTargets;

internal class SolidCirclePlatformTargetCreator : PlatformTargetCreator<SolidCirclePlatformTarget>
{
    protected override PlatformTarget CreatePlatformTargetFor(DrawContext drawContext, AssetRegistry assets)
    {
        return new SolidCirclePlatformTarget((SolidCircleContext)drawContext);
    }

    protected override bool Supports(DrawContext drawContext)
    {
        return drawContext is SolidCircleContext;
    }
}
