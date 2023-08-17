using Annex.Core.Scenes.Elements;
using Annex.Core.Scenes.Layouts.Html;

namespace SampleProject.Scenes.Level3
{
    internal class Level3 : Scene
    {
        public Level3(IHtmlSceneLoader htmlSceneLoader) {
            htmlSceneLoader.Load("level3.html", this);
        }
    }
}
