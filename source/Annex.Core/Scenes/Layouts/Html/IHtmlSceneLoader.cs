using Annex.Core.Scenes.Elements;

namespace Annex.Core.Scenes.Layouts.Html;

public interface IHtmlSceneLoader
{
    const string AssetProviderId = "html-scene-provider";

    void Load(string assetId, IScene scene);
}
