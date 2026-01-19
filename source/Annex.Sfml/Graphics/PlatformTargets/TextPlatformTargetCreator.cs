using Annex.Core.Assets;
using Annex.Core.Graphics.Contexts;

namespace Annex.Sfml.Graphics.PlatformTargets;

internal class TextPlatformTargetCreator : PlatformTargetCreator<TextPlatformTarget>
{
    protected override PlatformTarget CreatePlatformTargetFor(DrawContext drawContext, AssetRegistry assets)
    {
        var fonts = assets.GetFonts();
        return new TextPlatformTarget((TextContext)drawContext, fonts);
    }

    protected override bool Supports(DrawContext drawContext)
    {
        return drawContext is TextContext;
    }
}
