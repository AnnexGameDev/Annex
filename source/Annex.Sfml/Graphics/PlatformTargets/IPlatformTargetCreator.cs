using Annex.Core.Graphics.Contexts;

namespace Annex.Sfml.Graphics.PlatformTargets
{
    public interface IPlatformTargetCreator
    {
        bool TryGetOrCreate(DrawContext drawContext, out PlatformTarget? sfmlPlatformTarget);
    }
}
