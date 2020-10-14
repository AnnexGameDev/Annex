using Annex;
using Annex.Graphics.Contexts;
using Annex.Scenes.Components;
using Annex.Scenes.Layouts.Html;
using NUnit.Framework;
using System;
using System.Reflection;

namespace Tests.Scenes.Layouts
{
    public class HtmlLayoutLoaderTests
    {
        private HtmlLayoutLoader? _loader;
        private HtmlLayoutLoader Loader => this._loader!;

        [OneTimeSetUp]
        public void OneTimeSetUp() {
            var resolver = new UIElementTypeResolver(Assembly.GetExecutingAssembly(), this.GetType().Namespace!);
            this._loader = new HtmlLayoutLoader(resolver);
        }

        private SampleScene CreateScene(string xml) {
            var scene = new SampleScene();
            this.Loader.AddLayout(scene, xml);
            return scene;
        }

        [Test]
        public void AddLayout_EmptyString_ThrowsException() {
            Assert.Throws<AssertionFailedException>(() => CreateScene(""));
        }

        [Test]
        public void AddLayout_InvalidXML_ThrowsException() {
            Assert.Throws<AssertionFailedException>(() => CreateScene("<scene><scene>"));
        }

        [Test]
        public void AddLayout_EmptyScene_CreatesScene() {
            var scene = CreateScene(@"
<scene>
</scene>
");
            Assert.IsNotNull(scene);
        }

        [Test]
        public void AddLayout_SimpleImagePercentage_CreatesScene() {
            Random rng = new Random();
            int RandomPerc() => rng.Next(0, 100);

            string id = Guid.NewGuid().ToString();
            int p1 = RandomPerc();
            int p2 = RandomPerc();
            int p3 = RandomPerc();
            int p4 = RandomPerc();

            var scene = CreateScene(@$"
<scene>
    <picture
        id=""{id}""
        position=""{p1}%, {p2}%""
        size=""{p3}%, {p4}%""
    ></picture>
</scene>");

            var img = scene.GetElementById(id) as Image;
            Assert.IsNotNull(img);

            Assert.AreEqual(scene.Size.X * p1 / 100.0f, img.Position.X);
            Assert.AreEqual(scene.Size.Y * p2 / 100.0f, img.Position.Y);

            Assert.AreEqual(scene.Size.X * p3 / 100.0f, img.Size.X);
            Assert.AreEqual(scene.Size.Y * p4 / 100.0f, img.Size.Y);
        }

        [Test]
        public void AddLayout_SimpleImage_CreatesScene() {
            Random rng = new Random();
            int RandomLoc() => rng.Next(0, 960);

            string id = Guid.NewGuid().ToString();
            int p1 = RandomLoc();
            int p2 = RandomLoc();
            int p3 = RandomLoc();
            int p4 = RandomLoc();

            var scene = CreateScene(@$"
<scene>
    <picture
        id=""{id}""
        texture=""img.png""
        position=""{p1}, {p2}""
        size=""{p3}, {p4}""
    ></picture>
</scene>");

            var img = scene.GetElementById(id) as Image;
            Assert.IsNotNull(img);

            Assert.AreEqual(p1, img.Position.X);
            Assert.AreEqual(p2, img.Position.Y);

            Assert.AreEqual(p3, img.Size.X);
            Assert.AreEqual(p4, img.Size.Y);

            Assert.AreEqual("img.png", img.ImageTextureName.Value);
        }

        [Test]
        public void AddLayout_RelativeImagePercentages_CreatesScene() {
            Random rng = new Random();
            int RandomPerc() => rng.Next(0, 960);

            string imgId = Guid.NewGuid().ToString();
            string conId = Guid.NewGuid().ToString();
            int imgX = RandomPerc();
            int imgY = RandomPerc();
            int imgWidth = RandomPerc();
            int imgHeight = RandomPerc();
            int conX = RandomPerc();
            int conY = RandomPerc();
            int conWidth = RandomPerc();
            int conHeight = RandomPerc();

            var scene = CreateScene(@$"
<scene>
    <container
        id=""{conId}""
        position=""{conX}, {conY}""
        size=""{conWidth}%, {conHeight}%""
    >
        <picture
            id=""{imgId}""
            texture=""img.png""
            position=""{imgX}%, {imgY}%""
            size=""{imgWidth}%, {imgHeight}%""
        ></picture>
    </container>
</scene>");

            var img = scene.GetElementById(imgId) as Image;
            Assert.IsNotNull(img);

            var container = scene.GetElementById(conId) as Container;
            Assert.IsNotNull(container);

            Assert.AreEqual(container.Position.X + container.Size.X * imgX / 100.0f, img.Position.X);
            Assert.AreEqual(container.Position.Y + container.Size.Y * imgY / 100.0f, img.Position.Y);

            Assert.AreEqual(container.Size.X * imgWidth / 100.0f, img.Size.X);
            Assert.AreEqual(container.Size.Y * imgHeight / 100.0f, img.Size.Y);
        }

        [Test]
        public void AddLayout_RelativeImage_CreatesScene() {
            Random rng = new Random();
            int RandomLoc() => rng.Next(0, 960);

            string imgId = Guid.NewGuid().ToString();
            string conId = Guid.NewGuid().ToString();
            int imgX = RandomLoc();
            int imgY = RandomLoc();
            int imgWidth = RandomLoc();
            int imgHeight = RandomLoc();
            int conX = RandomLoc();
            int conY = RandomLoc();
            int conWidth = RandomLoc();
            int conHeight = RandomLoc();

            var scene = CreateScene(@$"
<scene>
    <container
        id=""{conId}""
        position=""{conX}, {conY}""
        size=""{conWidth}, {conHeight}""
    >
        <picture
            id=""{imgId}""
            texture=""img.png""
            position=""{imgX}, {imgY}""
            size=""{imgWidth}, {imgHeight}""
        ></picture>
    </container>
</scene>");

            var img = scene.GetElementById(imgId) as Image;
            Assert.IsNotNull(img);

            var container = scene.GetElementById(conId) as Container;
            Assert.IsNotNull(container);

            Assert.AreEqual(container.Position.X + imgX, img.Position.X);
            Assert.AreEqual(container.Position.Y + imgY, img.Position.Y);
        }

        [Test]
        public void AddLayout_SimpleTextbox_CreatesScene() {
            var rng = new Random();

            string id = Guid.NewGuid().ToString();
            string text = Guid.NewGuid().ToString();
            string font = Guid.NewGuid().ToString();
            int fontsize = rng.Next(0, 100);

            var scene = CreateScene(@$"
<scene>
    <textbox
        id=""{id}""
        text=""{text}""
        font=""{font}""
        font-size=""{fontsize}""
    ></textbox>
</scene>
");
            var textbox = scene.GetElementById(id) as Textbox;
            Assert.IsNotNull(textbox);

            Assert.AreEqual(text, textbox.Text.Value);
            Assert.AreEqual(font, textbox.Font.Value);
            Assert.AreEqual(fontsize, textbox.FontSize.Value);
        }

        [Test]
        public void AddLayout_TextboxWithNoFont_UsesDefault() {
            string id = Guid.NewGuid().ToString();

            var scene = CreateScene(@$"
<scene>
    <textbox
        id=""{id}""
    ></textbox>
</scene>
");
            var textbox = scene.GetElementById(id) as Textbox;
            Assert.IsNotNull(textbox);

            Assert.AreEqual("default.ttf", textbox.Font.Value);
        }

        [Test]
        public void AddLayout_EveryPossibleTextAlignmentVarient_CreatesScene() {
            var has = Enum.GetValues(typeof(HorizontalAlignment));
            var vas = Enum.GetValues(typeof(VerticalAlignment));

            foreach (var ha in has) {
                foreach (var va in vas) {
                    string id = Guid.NewGuid().ToString();
                    var scene = CreateScene(@$"
<scene>
    <textbox
        id=""{id}""
        text-alignment=""{va}, {ha}""
    ></textbox>
</scene>
");
                    var textbox = scene.GetElementById(id) as Textbox;
                    Assert.IsNotNull(textbox);

                    Assert.AreEqual(va, textbox.TextAlignment.VerticalAlignment);
                    Assert.AreEqual(ha, textbox.TextAlignment.HorizontalAlignment);
                }
            }
        }

        [Test]
        public void AddLayout_ElementBinding_CreatesScene() {
            string id = Guid.NewGuid().ToString();
            var scene = CreateScene(@$"
<scene>
    <button
        id=""{id}""
        class=""{nameof(SimpleButton)}""
    ></button>
</scene>
");
            var button = scene.GetElementById(id) as SimpleButton;
            Assert.IsNotNull(button);
        }

        private class SampleScene : Scene
        {
            public SampleScene() : base(960, 640) {
            }
        }

    }

    public class SimpleButton : Button
    {
        public SimpleButton(string id) : base(id) {
        }
    }
}
