using Annex.Core.Graphics.Contexts;
using Annex.Sfml.Collections.Generic;

namespace Annex.Sfml.Graphics.PlatformTargets;

internal class TextPlatformTargetCreator : PlatformTargetCreator<TextPlatformTarget>
{
    private readonly IFontCache _fontCache;

    public TextPlatformTargetCreator(IFontCache fontCache)
    {
        _fontCache = fontCache;
    }

    protected override PlatformTarget CreatePlatformTargetFor(DrawContext drawContext)
    {
        return new TextPlatformTarget((TextContext)drawContext, _fontCache);
    }

    protected override bool Supports(DrawContext drawContext)
    {
        return drawContext is TextContext;
    }
}
