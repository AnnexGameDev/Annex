namespace SampleProject.Assets
{
    public class IconManager : AssetManager, IIconManager
    {
        public IconManager() : base(new FileSystemStreamer("icons", ".png")) {
        }
    }
}
