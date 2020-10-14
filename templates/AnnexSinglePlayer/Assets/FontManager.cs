using Annex.Assets;
using Annex.Assets.Loaders;
using Annex.Assets.Services;

namespace AnnexSinglePlayer.Assets
{
    public class FontManager : AssetManager, IFontManager
    {
        public FontManager() : base(new FileSystemStreamer("fonts", ".ttf")) {
        }
    }
}
