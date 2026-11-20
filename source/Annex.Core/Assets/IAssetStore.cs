namespace Annex.Core.Assets;

public interface IAssetStore : IDisposable
{
    string Id { get; }
    object GetUntyped(string id);
}

public interface IAssetStore<T> : IAssetStore where T : notnull
{
    T Get(string id);
}