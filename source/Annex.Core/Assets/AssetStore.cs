using System.Collections.Concurrent;

namespace Annex.Core.Assets;

public class AssetStore<T> : IAssetStore<T> where T : notnull
{
    public string Id { get; }

    private readonly IDictionary<string, T> _assets = new ConcurrentDictionary<string, T>();
    private readonly Func<string, T> _loader;

    public AssetStore(string id, Func<string, T> loader)
    {
        Id = id;
        _loader = loader;
    }

    public T Get(string id)
    {
        if (!_assets.ContainsKey(id))
        {
            _assets.Add(id, _loader(id));
        }
        return _assets[id];
    }

    public void Dispose()
    {
        foreach (var entry in _assets.Values)
        {
            if (entry is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
        _assets.Clear();
    }

    public object GetUntyped(string id) => Get(id);
}
