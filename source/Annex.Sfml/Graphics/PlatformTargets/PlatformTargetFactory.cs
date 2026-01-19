using Annex.Core.Assets;
using Annex.Core.Graphics.Contexts;

namespace Annex.Sfml.Graphics.PlatformTargets;

internal class PlatformTargetFactory : IPlatformTargetFactory
{
    private readonly IEnumerable<IPlatformTargetCreator> _sfmlPlatformTargetCreators;

    public PlatformTargetFactory(IEnumerable<IPlatformTargetCreator> sfmlPlatformTargetCreators)
    {
        _sfmlPlatformTargetCreators = sfmlPlatformTargetCreators;
    }

    public PlatformTarget? GetPlatformTarget(DrawContext context, AssetRegistry assets)
    {
        foreach (var creator in _sfmlPlatformTargetCreators)
        {
            if (creator.TryGetOrCreate(context, assets, out var sfmlPlatformTarget))
            {
                return sfmlPlatformTarget;
            }
        }
        throw new InvalidOperationException($"Unable to get sfml platform target for {context}");
    }
}
