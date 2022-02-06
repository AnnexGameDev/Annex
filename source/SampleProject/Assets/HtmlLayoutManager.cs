namespace SampleProject.Assets
{
    public class HtmlLayoutManager : AssetManager, IHtmlLayoutManager
    {
        public HtmlLayoutManager() : base(new FileSystemStreamer("layouts", ".html")) {
        }
    }
}
