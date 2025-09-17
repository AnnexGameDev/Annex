using Annex.Core;
using Annex.Core.Assets;
using Annex.Core.Graphics;
using SFML.Graphics;

namespace Annex.Sfml.Graphics;

internal class FontAssetProvider : AssetProvider<Font>
{
    private readonly IEnumerable<IAssetProvider> _assetProviders;
    private AssetProvider<object>? _fontAssetProvider;

    public FontAssetProvider(IEnumerable<IAssetProvider> assetProviders) : base("sfml-font-asset-provider")
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
