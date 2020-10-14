using Annex.Assets;
using Annex.Assets.Streams;
using Annex.Assets.Services;

namespace SampleProject.Assets
{
    public class IconManager : AssetManager, IIconManager
    {
        public IconManager() : base(new FileSystemStreamer("icons", ".png")) {
        }
    }
}
