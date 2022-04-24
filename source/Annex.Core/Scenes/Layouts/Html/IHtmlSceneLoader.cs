using Annex.Core.Scenes.Components;

namespace Annex.Core.Scenes.Layouts.Html
{
    public interface IHtmlSceneLoader
    {
        void Load(string assetId, IScene scene);
    }
}
