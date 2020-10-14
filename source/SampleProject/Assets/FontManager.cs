using Annex.Assets;
using Annex.Assets.Streams;
using Annex.Assets.Services;

namespace SampleProject.Assets
{
    public class FontManager : AssetManager, IFontManager
    {
        public FontManager() : base(new FileSystemStreamer("fonts", ".ttf")) {
        }
    }
}
