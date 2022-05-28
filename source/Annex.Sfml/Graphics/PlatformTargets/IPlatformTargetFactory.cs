using Annex_Old.Core.Graphics.Contexts;

namespace Annex_Old.Sfml.Graphics.PlatformTargets
{
    public interface IPlatformTargetFactory
    {
        PlatformTarget? GetPlatformTarget(DrawContext context);
    }
}
