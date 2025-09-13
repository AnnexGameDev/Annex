using Annex.Core.Graphics.Contexts;
using Annex.Sfml.Collections.Generic;

namespace Annex.Sfml.Graphics.PlatformTargets;

internal class TexturePlatformTargetCreator : PlatformTargetCreator<TexturePlatformTarget>
{
    private readonly ITextureCache _textureCache;

    public TexturePlatformTargetCreator(ITextureCache textureCache)
    {
        _textureCache = textureCache;
    }

    protected override PlatformTarget CreatePlatformTargetFor(DrawContext drawContext)
    {
        return new TexturePlatformTarget((TextureContext)drawContext, _textureCache);
    }

    protected override bool Supports(DrawContext drawContext)
    {
        return drawContext is TextureContext;
    }
}
