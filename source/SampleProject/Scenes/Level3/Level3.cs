﻿using Annex.Core.Helpers;
using Annex.Core.Scenes.Elements;

namespace SampleProject.Scenes.Level3
{
    internal class Level3 : Scene
    {
        public Level3() {
            HtmlSceneLoaderHelper.Load("level3.html", this);
        }
    }
}
