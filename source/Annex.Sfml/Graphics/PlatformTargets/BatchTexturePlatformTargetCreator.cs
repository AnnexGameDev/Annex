using Annex.Core.Graphics.Contexts;

namespace Annex.Sfml.Graphics.PlatformTargets;

internal class BatchTexturePlatformTargetCreator : PlatformTargetCreator<BatchTexturePlatformTarget>
{
    private readonly TextureAssetProvider _textureAssetProvider;

    public BatchTexturePlatformTargetCreator(TextureAssetProvider textureAssetProvider)
    {
        _textureAssetProvider = textureAssetProvider;
    }

    protected override PlatformTarget CreatePlatformTargetFor(DrawContext drawContext)
    {
        return new BatchTexturePlatformTarget((BatchTextureContext)drawContext, _textureAssetProvider);
    }

    protected override bool Supports(DrawContext drawContext)
    {
        return drawContext is BatchTextureContext;
    }
}
