using Annex_Old.Core.Graphics.Contexts;

namespace Annex_Old.Sfml.Graphics.PlatformTargets
{
    public interface IPlatformTargetCreator
    {
        bool TryGetOrCreate(DrawContext drawContext, out PlatformTarget? sfmlPlatformTarget);
    }
}
