using Annex.Assets;
using Annex.Assets.Streams;
using Annex.Scenes.Layouts.Html;

namespace SampleProject.Assets
{
    public class HtmlLayoutManager : AssetManager, IHtmlLayoutManager
    {
        public HtmlLayoutManager() : base(new FileSystemStreamer("layouts", ".html")) {
        }
    }
}
