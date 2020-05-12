#nullable enable
namespace Annex.Assets
{
    public interface IAssetInitializer
    {
        bool Validate(IAssetInitializerArgs args);
        object? Load(IAssetInitializerArgs args, IAssetLoader assetLoader);
    }
}
