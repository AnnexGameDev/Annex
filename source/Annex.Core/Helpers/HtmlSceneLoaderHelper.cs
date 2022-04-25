using Annex.Core.Scenes.Components;
using Annex.Core.Scenes.Layouts.Html;

namespace Annex.Core.Helpers
{
    public class HtmlSceneLoaderHelper
    {
        private static IHtmlSceneLoader? _htmlSceneLoader;

        public HtmlSceneLoaderHelper(IHtmlSceneLoader htmlSceneLoader) {
            _htmlSceneLoader = htmlSceneLoader;
        }

        public static void Load(string assetId, IScene scene) {
            _htmlSceneLoader?.Load(assetId, scene);
        }
    }
}
