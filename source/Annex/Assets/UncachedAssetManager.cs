using Annex_Old.Assets.Converters;
using Annex_Old.Assets.Streams;
using System;

namespace Annex_Old.Assets
{
    public class UncachedAssetManager : IAssetManager
    {
        public IDataStreamer DataStreamer { get; }

        public UncachedAssetManager(IDataStreamer streamer) {
            this.DataStreamer = streamer;
        }

        public void Destroy() {
        }

        // TODO: This never returns false
        public bool GetAsset(AssetConverterArgs args, out Asset asset) {
            var data = this.DataStreamer.Read(args.Id);
            asset = args.Converter.CreateAsset(args.Id, data);
            return true;
        }

        public Asset[] GetCachedAssets() {
            return Array.Empty<Asset>();
        }

        public void UnloadCachedAsset(string id) {
        }
    }
}
