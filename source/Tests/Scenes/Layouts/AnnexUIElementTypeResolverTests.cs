using Annex;
using Annex.Scenes.Components;
using Annex.Scenes.Layouts.Html;
using NUnit.Framework;
using System;

namespace Tests.Scenes.Layouts
{
    public class AnnexUIElementTypeResolverTests
    {
        private Type[] AnnexUITypes => new Type[] {
                typeof(Button),
                typeof(Container),
                typeof(Image),
                typeof(Label),
                typeof(Textbox),
            };

        [Test]
        public void Resolve_LowercaseNames_ThrowsException() {
            var resolver = new AnnexUIElementTypeResolver();

            foreach (var type in this.AnnexUITypes) {
                Assert.Throws<AssertionFailedException>(() => resolver.Resolve(type.Name.ToLower()));
            }
        }

        [Test]
        public void Resolve_UppercaseNames_ThrowsException() {
            var resolver = new AnnexUIElementTypeResolver();

            foreach (var type in this.AnnexUITypes) {
                Assert.Throws<AssertionFailedException>(() => resolver.Resolve(type.Name.ToUpper()));
            }
        }

        [Test]
        public void Resolve_ValidNames_ReturnsType() {
            var resolver = new AnnexUIElementTypeResolver();

            foreach (var type in this.AnnexUITypes) {
                Assert.AreEqual(type, resolver.Resolve(type.Name));
            }
        }
    }
}
