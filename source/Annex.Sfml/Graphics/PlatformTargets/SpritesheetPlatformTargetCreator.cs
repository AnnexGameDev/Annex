using Annex.Core.Graphics.Contexts;

namespace Annex.Sfml.Graphics.PlatformTargets;

internal class SpritesheetPlatformTargetCreator : PlatformTargetCreator<SpritesheetPlatformTarget>
{
    private readonly TextureAssetProvider _textureAssetProvider;

    public SpritesheetPlatformTargetCreator(TextureAssetProvider textureAssetProvider)
    {
        _textureAssetProvider = textureAssetProvider;
    }

    protected override PlatformTarget CreatePlatformTargetFor(DrawContext drawContext)
    {
        return new SpritesheetPlatformTarget((SpritesheetContext)drawContext, _textureAssetProvider);
    }

    protected override bool Supports(DrawContext drawContext)
    {
        return drawContext is SpritesheetContext;
    }
}
