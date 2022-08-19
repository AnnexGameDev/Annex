using Annex_Old.Assets.Converters;
using Annex_Old.Assets.Streams;
using Annex_Old.Services;

namespace Annex_Old.Assets
{
    public interface IAssetManager : IService
    {
        IDataStreamer DataStreamer { get; }
        bool GetAsset(AssetConverterArgs args, out Asset asset);
        Asset[] GetCachedAssets();
        void UnloadCachedAsset(string id);
    }
}
