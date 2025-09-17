using Scaffold.Data.Serialization;
using Scaffold.Logging;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;

namespace Annex.Core.Assets;

public abstract class AssetProvider<T> : IAssetProvider
{
    public string ProviderId { get; }
    private readonly IDictionary<string, T> _assets = new ConcurrentDictionary<string, T>();
    private readonly Func<string, T>? _assetLoader;

    public AssetProvider(string providerId) : this(providerId, null)
    {
    }

    public AssetProvider(string providerId, Func<string, T>? assetLoader)
    {
        ProviderId = providerId;
        _assetLoader = assetLoader;
    }

    public bool TryGetAsset(string id, [NotNullWhen(true)] out T? result)
    {
        if (!_assets.ContainsKey(id))
        {
            try
            {
                if (!ValidateAssetsSecurity(id))
                {
                    result = default;
                    return false;
                }

                _assets.Add(id, LoadAsset(id));
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                result = default;
                return false;
            }
        }
        result = _assets[id]!;
        return true;
    }

    /// <summary>
    /// Verifying that this is indeed an asset we are allowed to load.
    /// </summary>
    protected virtual bool ValidateAssetsSecurity(string id) => true;

    protected virtual T LoadAsset(string id) => _assetLoader!.Invoke(id);
}

public static class AssetProvider
{
    public static AssetsFolder<T> FromFolder<T>(string providerId, string filter, string path, Func<string, T> assetLoader)
    {
        return new AssetsFolder<T>(providerId, filter, path, assetLoader);
    }
}


public static class AssetLoadingStrategy
{
    public static T JsonSerialization<T>(string assetId)
    {
        return Json.Deserialize<T>(File.ReadAllText(assetId));
    }

    public static string ReadAllString(string assetId)
    {
        return File.ReadAllText(assetId);
    }
}