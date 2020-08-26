namespace Annex.Assets
{
    public interface IAssetManager
    {
        AssetType AssetType { get; set; }
        bool GetAsset(AssetInitializerArgs args, out object? asset);
        void PackageAssetsToBinaryFrom(string path);
    }
}
