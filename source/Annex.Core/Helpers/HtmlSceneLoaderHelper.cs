﻿using Annex.Core.Scenes.Elements;
using Annex.Core.Scenes.Layouts.Html;

namespace Annex.Core.Helpers
{
    public class HtmlSceneLoaderHelper
    {
        private static IHtmlSceneLoader? _htmlSceneLoader;

        public HtmlSceneLoaderHelper(IHtmlSceneLoader htmlSceneLoader) {
            if (_htmlSceneLoader != null) {
                throw new InvalidOperationException("Static helper resource is already instanciated");
            }
            _htmlSceneLoader = htmlSceneLoader;
        }

        public static void Load(string assetId, IScene scene) {
            _htmlSceneLoader?.Load(assetId, scene);
        }
    }
}
