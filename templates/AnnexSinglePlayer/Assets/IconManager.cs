using Annex.Assets;
using Annex.Assets.Loaders;
using Annex.Assets.Services;

namespace AnnexSinglePlayer.Assets
{
    public class IconManager : AssetManager, IIconManager
    {
        public IconManager() : base(new FileSystemStreamer("icons", ".png")) {
        }
    }
}
