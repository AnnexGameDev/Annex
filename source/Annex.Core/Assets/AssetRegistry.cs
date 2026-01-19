namespace Annex.Core.Assets;

public class AssetRegistry : IDisposable
{
    public const string Fonts = nameof(Fonts);
    public const string Textures = nameof(Textures);
    public const string HtmlScenes = nameof(HtmlScenes);

    private readonly Dictionary<string, IAssetStore> _assetStores = new();

    public IAssetStore GetFonts() => Get(Fonts);
    public IAssetStore GetTextures() => Get(Textures);
    public IAssetStore GetHtmlScenes() => Get(HtmlScenes);

    private IAssetStore Get(string id) => _assetStores[id];

    public void Add(IAssetStore assetStore) => _assetStores.Add(assetStore.Id, assetStore);

    public void Dispose()
    {
        foreach (var assetStore in _assetStores.Values)
        {
            assetStore.Dispose();
        }
        _assetStores.Clear();
    }
}
