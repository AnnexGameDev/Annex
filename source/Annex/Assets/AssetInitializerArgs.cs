namespace Annex.Assets
{
    public class AssetInitializerArgs
    {
        public string Key { get; set; }

        public AssetInitializerArgs(string key) {
            this.Key = key;
        }
    }
}
