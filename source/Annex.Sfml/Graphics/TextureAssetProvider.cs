using Annex.Core;
using Annex.Core.Assets;
using Annex.Core.Graphics;
using SFML.Graphics;

namespace Annex.Sfml.Graphics;

internal class TextureAssetProvider : AssetProvider<Texture>
{
    private readonly IEnumerable<IAssetProvider> _assetProviders;
    private IAssetProvider<object>? _textureProvider;

    public TextureAssetProvider(IEnumerable<IAssetProvider> assetProviders) : base("sfml-texture-asset-provider")
    {
        _assetProviders = assetProviders;
    }

    protected override Texture LoadAsset(string id)
    {
        _textureProvider ??= _assetProviders.GetProvider<object>(IGraphicsEngine.TextureAssetProviderId);
        _textureProvider!.TryGetAsset(id, out object? result);
        return (Texture)result!;
    }
}
