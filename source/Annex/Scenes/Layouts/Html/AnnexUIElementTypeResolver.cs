using Annex.Scenes.Components;
using System.Reflection;

namespace Annex.Scenes.Layouts.Html
{
    public class AnnexUIElementTypeResolver : UIElementTypeResolver
    {
        public AnnexUIElementTypeResolver() : base(Assembly.GetExecutingAssembly(), typeof(Scene).Namespace!) {
        }
    }
}
