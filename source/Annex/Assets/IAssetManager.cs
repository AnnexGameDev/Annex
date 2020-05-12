#nullable enable
namespace Annex.Assets
{
    public interface IAssetManager
    {
        bool GetAsset(IAssetInitializerArgs args, out object? asset);
    }
}
