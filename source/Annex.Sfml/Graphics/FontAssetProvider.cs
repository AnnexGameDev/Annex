using Annex.Core;
using Annex.Core.Assets;
using Annex.Core.Graphics;
using Annex.Core.Time;
using SFML.Graphics;

namespace Annex.Sfml.Graphics;

internal class FontAssetProvider : AssetProvider<Font>
{
    private readonly IEnumerable<IAssetProvider> _assetProviders;
    private IAssetProvider<object>? _fontAssetProvider;

    public FontAssetProvider(IEnumerable<IAssetProvider> assetProviders, ITimeService timeService) : base("sfml-font-asset-provider", timeService)
    {
        _assetProviders = assetProviders;
    }

    protected override Font LoadAsset(string id)
    {
        _fontAssetProvider ??= _assetProviders.GetProvider<object>(IGraphicsEngine.FontAssetProviderId);
        _fontAssetProvider!.TryGetAsset(id, out object? result);
        return (Font)result!;
    }
}
