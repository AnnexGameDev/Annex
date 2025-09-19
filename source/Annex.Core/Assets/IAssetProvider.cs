using System.Diagnostics.CodeAnalysis;

namespace Annex.Core.Assets;

public interface IAssetProvider
{
    string ProviderId { get; }
}

public interface IAssetProvider<T> : IAssetProvider
{
    bool TryGetAsset(string id, [NotNullWhen(true)] out T? result);
}