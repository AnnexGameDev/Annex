using Annex_Old.Core.Helpers;
using Annex_Old.Core.Scenes.Components;

namespace SampleProject.Scenes.Level3
{
    internal class Level3 : Scene
    {
        public Level3() {
            HtmlSceneLoaderHelper.Load("level3.html", this);
        }
    }
}
