using Annex.Core.Graphics.Contexts;

namespace Annex.Sfml.Graphics.PlatformTargets;

internal class TexturePlatformTargetCreator : PlatformTargetCreator<TexturePlatformTarget>
{
    private readonly TextureAssetProvider _textureAssetProvider;

    public TexturePlatformTargetCreator(TextureAssetProvider textureAssetProvider)
    {
        _textureAssetProvider = textureAssetProvider;
    }

    protected override PlatformTarget CreatePlatformTargetFor(DrawContext drawContext)
    {
        return new TexturePlatformTarget((TextureContext)drawContext, _textureAssetProvider);
    }

    protected override bool Supports(DrawContext drawContext)
    {
        return drawContext is TextureContext;
    }
}
