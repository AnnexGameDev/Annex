using Annex_Old.Scenes.Components;
using System.Reflection;

namespace Annex_Old.Scenes.Layouts.Html
{
    public class AnnexUIElementTypeResolver : UIElementTypeResolver
    {
        public AnnexUIElementTypeResolver() : base(Assembly.GetExecutingAssembly(), typeof(Scene).Namespace!) {
        }
    }
}
