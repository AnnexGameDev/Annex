namespace Annex_Old.Assets.Converters
{
    public class AssetConverterArgs
    {
        public string Id { get; }
        public IAssetConverter Converter;

        public AssetConverterArgs(string key, IAssetConverter converter) {
            this.Id = key.ToLower();
            this.Converter = converter;
        }
    }
}
