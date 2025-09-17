using Annex.Core.Graphics.Contexts;

namespace Annex.Sfml.Graphics.PlatformTargets;

internal class TextPlatformTargetCreator : PlatformTargetCreator<TextPlatformTarget>
{
    private readonly FontAssetProvider _fontAssetProvider;

    public TextPlatformTargetCreator(FontAssetProvider fontAssetProvider)
    {
        _fontAssetProvider = fontAssetProvider;
    }

    protected override PlatformTarget CreatePlatformTargetFor(DrawContext drawContext)
    {
        return new TextPlatformTarget((TextContext)drawContext, _fontAssetProvider);
    }

    protected override bool Supports(DrawContext drawContext)
    {
        return drawContext is TextContext;
    }
}
