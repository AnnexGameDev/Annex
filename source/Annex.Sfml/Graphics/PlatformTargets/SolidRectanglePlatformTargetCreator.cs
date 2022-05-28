using Annex_Old.Core.Graphics.Contexts;

namespace Annex_Old.Sfml.Graphics.PlatformTargets
{
    internal class SolidRectanglePlatformTargetCreator : PlatformTargetCreator<SolidRectanglePlatformTarget>
    {
        protected override PlatformTarget CreatePlatformTargetFor(DrawContext drawContext) {
            return new SolidRectanglePlatformTarget((SolidRectangleContext)drawContext);
        }

        protected override bool Supports(DrawContext drawContext) {
            return drawContext is SolidRectangleContext;
        }
    }
}
