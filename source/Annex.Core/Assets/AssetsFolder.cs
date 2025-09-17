namespace Annex.Core.Assets;

public class AssetsFolder<T> : AssetProvider<T>
{
    private readonly string _basePath;
    private readonly string _assetFilter;

    public AssetsFolder(string providerId, string path, string filter, Func<string, T> assetLoader) : base(providerId, assetLoader)
    {
        _basePath = path;
        _assetFilter = filter;
    }

    protected override bool ValidateAssetsSecurity(string id)
    {
        var assetPath = Path.Combine(_basePath, id);
        var fi = new FileInfo(assetPath);

        if (fi.Extension != _assetFilter)
        {
            return false;
        }

        if (!fi.Exists)
        {
            return false;
        }

        // Make sure there's no ..'s
        if (!fi.FullName.StartsWith(_basePath))
        {
            return false;
        }
        return true;
    }

    protected override T LoadAsset(string id)
    {
        return base.LoadAsset(Path.Combine(_basePath, id));
    }
}