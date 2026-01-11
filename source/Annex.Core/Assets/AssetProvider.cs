using Annex.Core.Time;
using Scaffold.Data.Serialization;
using Scaffold.Logging;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;

namespace Annex.Core.Assets;

public abstract class AssetProvider<T> : IAssetProvider<T>
{
    private readonly ITimeService _timeService;

    public string ProviderId { get; }
    private readonly IDictionary<string, CacheEntry<T>> _assets = new ConcurrentDictionary<string, CacheEntry<T>>();
    private readonly Func<string, T>? _assetLoader;

    public AssetProvider(string providerId, ITimeService timeService) : this(providerId, timeService, null)
    {
    }

    public AssetProvider(string providerId, ITimeService timeService, Func<string, T>? assetLoader)
    {
        _timeService = timeService;
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

                _assets.Add(id, new CacheEntry<T>(LoadAsset(id), _timeService.Now));
            }
            catch (Exception ex)
            {
                Log.Exception(ex);
                result = default;
                return false;
            }
        }
        var entry = _assets[id]!;
        entry.Hit(_timeService.Now);
        result = entry.Value;
        return true;
    }

    /// <summary>
    /// Verifying that this is indeed an asset we are allowed to load.
    /// </summary>
    protected virtual bool ValidateAssetsSecurity(string id) => true;

    protected virtual T LoadAsset(string id) => _assetLoader!.Invoke(id);

    public void PurgeAssetsNotUsedSince(long time)
    {
        foreach (var entry in _assets.ToArray())
        {
            if (entry.Value.LastHit <= time)
            {
                Console.WriteLine($"Getting rid of: {entry.Key}");
                if (entry.Value is IDisposable disposable)
                {
                    disposable.Dispose();
                }
                _assets.Remove(entry.Key);
            }
        }
    }
}

public static class AssetProvider
{
    public static AssetsFolder<T> FromFolder<T>(string providerId, string filter, string path, ITimeService timeService, Func<string, T> assetLoader)
    {
        return new AssetsFolder<T>(providerId, filter, path, timeService,  assetLoader);
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