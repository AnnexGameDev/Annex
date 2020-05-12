#nullable enable
using Annex.Assets.Loaders;

namespace Annex.Assets.Managers
{
    public class PakAssetManager : CachedAssetManager
    {
        public PakAssetManager(AssetType type, string pakFilePath, IAssetInitializer assetLoader)
            : base(type, new PakLoader(PakFile.CreateForInput(pakFilePath)), assetLoader) {
        }
    }
}
