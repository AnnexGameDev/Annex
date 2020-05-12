#nullable enable
namespace Annex.Assets
{
    public interface IAssetInitializer
    {
        string AssetPath { get; set; }
        bool Validate(IAssetInitializerArgs args);
        object? Load(IAssetInitializerArgs args, IAssetLoader assetLoader);
    }
}
