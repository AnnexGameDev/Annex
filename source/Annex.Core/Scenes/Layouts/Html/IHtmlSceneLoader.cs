using Annex_Old.Core.Scenes.Components;

namespace Annex_Old.Core.Scenes.Layouts.Html
{
    public interface IHtmlSceneLoader
    {
        void Load(string assetId, IScene scene);
    }
}
