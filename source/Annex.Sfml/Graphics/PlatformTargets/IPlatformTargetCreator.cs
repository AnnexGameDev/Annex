using Annex.Core.Assets;
using Annex.Core.Graphics.Contexts;

namespace Annex.Sfml.Graphics.PlatformTargets;

internal interface IPlatformTargetCreator
{
    bool TryGetOrCreate(DrawContext drawContext, AssetRegistry assets, out PlatformTarget? sfmlPlatformTarget);
}
