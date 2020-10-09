using Annex.Scenes.Layouts.Html;
using NUnit.Framework;
using System.Reflection;

namespace Tests.Scenes.Layouts
{
    public class UIElementTypeResolverTests
    {
        [Test]
        public void Resolve_ThisClass_ReturnsType() {
            var resolver = new UIElementTypeResolver(Assembly.GetExecutingAssembly(), this.GetType().Namespace!);
            Assert.AreEqual(typeof(UIElementTypeResolverTests), resolver.Resolve(nameof(UIElementTypeResolverTests)));
        }
    }
}
