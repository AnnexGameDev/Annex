namespace SampleProject.Assets
{
    public class FontManager : AssetManager, IFontManager
    {
        public FontManager() : base(new FileSystemStreamer("fonts", ".ttf")) {
        }
    }
}
