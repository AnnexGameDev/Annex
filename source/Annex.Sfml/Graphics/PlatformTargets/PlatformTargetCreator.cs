using Annex.Core.Assets;
using Annex.Core.Graphics.Contexts;

namespace Annex.Sfml.Graphics.PlatformTargets;

internal abstract class PlatformTargetCreator<TPlatformTarget> : IPlatformTargetCreator where TPlatformTarget : PlatformTarget
{
    public bool TryGetOrCreate(DrawContext drawContext, AssetRegistry assets, out PlatformTarget? platformTarget)
    {
        platformTarget = default;

        if (!Supports(drawContext))
            return false;

        if (GetExistingPlatformTarget(drawContext) is TPlatformTarget existingPlatformTarget)
        {
            platformTarget = existingPlatformTarget;
            return true;
        }

        var newPlatformTarget = CreatePlatformTargetFor(drawContext, assets);
        drawContext.SetPlatformTarget(newPlatformTarget);
        platformTarget = newPlatformTarget;
        return true;
    }

    protected abstract PlatformTarget CreatePlatformTargetFor(DrawContext drawContext, AssetRegistry assets);
    protected abstract bool Supports(DrawContext drawContext);

    private static TPlatformTarget? GetExistingPlatformTarget(DrawContext context)
    {
        return context.PlatformTarget as TPlatformTarget;
    }
}
