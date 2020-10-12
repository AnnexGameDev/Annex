using Annex.Assets.Converters;
using Annex.Assets.Streams;
using Annex.Services;

namespace Annex.Assets
{
    public interface IAssetManager : IService
    {
        IDataStreamer DataStreamer { get; }
        bool GetAsset(AssetConverterArgs args, out Asset asset);
        Asset[] GetCachedAssets();
        void UnloadCachedAsset(string id);
    }
}
