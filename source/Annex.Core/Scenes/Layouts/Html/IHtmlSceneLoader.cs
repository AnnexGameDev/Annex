﻿using Annex.Core.Scenes.Elements;

namespace Annex.Core.Scenes.Layouts.Html
{
    public interface IHtmlSceneLoader
    {
        void Load(string assetId, IScene scene);
    }
}
