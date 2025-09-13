using Annex.Core.Graphics.Contexts;

namespace Annex.Sfml.Graphics.PlatformTargets;

internal interface IPlatformTargetFactory
{
    PlatformTarget? GetPlatformTarget(DrawContext context);
}
