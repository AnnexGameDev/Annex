using System.Diagnostics.CodeAnalysis;

namespace Annex.Core.Assets;

public interface IAssetProvider
{
    string ProviderId { get; }
    void PurgeAssetsNotUsedSince(long time);
}

public interface IAssetProvider<T> : IAssetProvider
{
    bool TryGetAsset(string id, [NotNullWhen(true)] out T? result);
}