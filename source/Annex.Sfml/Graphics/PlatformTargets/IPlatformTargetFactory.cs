using Annex.Core.Graphics.Contexts;

namespace Annex.Sfml.Graphics.PlatformTargets
{
    public interface IPlatformTargetFactory
    {
        PlatformTarget GetPlatformTarget(DrawContext context);
    }
}
