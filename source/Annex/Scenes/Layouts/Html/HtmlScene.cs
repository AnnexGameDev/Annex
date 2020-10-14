using Annex.Assets.Converters;
using Annex.Scenes.Components;
using Annex.Services;
using System.Reflection;

namespace Annex.Scenes.Layouts.Html
{
    public abstract class HtmlScene : Scene
    {
        public HtmlScene(string assetId) {
            var typeResolver = new UIElementTypeResolver(Assembly.GetCallingAssembly(), this.GetType().Namespace!);

            var layoutManager = ServiceContainerSingleton.Instance?.Resolve<IHtmlLayoutManager>();
            Debug.ErrorIf(layoutManager == null, $"Unable to resolve {nameof(IHtmlLayoutManager)}");

            if (!layoutManager!.GetAsset(new AssetConverterArgs(assetId, new UTF8Converter()), out var asset)) {
                Debug.Error($"Unable to get asset {assetId}");
            }
            string target = (string)asset.GetTarget();

            new HtmlLayoutLoader(typeResolver).AddLayout(this, target);
        }
    }
}
