namespace Annex.Assets
{
    public interface IAssetInitializer
    {
        string AssetPath { get; set; }
        bool Validate(AssetInitializerArgs args);
        object? Load(AssetInitializerArgs args, IAssetLoader assetLoader);
    }
}
