using Annex.Data;
using Annex.Data.Shared;
using Annex.Graphics.Contexts;
using Annex.Scenes.Components;
using Annex.Services;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Annex.Scenes.Layouts.Html
{
    public class HtmlLayoutLoader
    {
        private readonly UIElementTypeResolver _customTypeResolver;
        private readonly AnnexUIElementTypeResolver _annexTypeResolver;
        private readonly UIElementActivator _uiElementActivator;
        private readonly HtmlElementToAnnexTypeMap _elementToTypeMap;
        private readonly Dictionary<string, HtmlAttributes> _styles;

        public HtmlLayoutLoader(UIElementTypeResolver typeResolver) {
            this._customTypeResolver = typeResolver;
            this._annexTypeResolver = new AnnexUIElementTypeResolver();
            this._uiElementActivator = new UIElementActivator();
            this._styles = new Dictionary<string, HtmlAttributes>();

            this._elementToTypeMap = new HtmlElementToAnnexTypeMap(
                "container",
                "button",
                "label",
                "textbox");
            this._elementToTypeMap.AddRelation("picture", "image");
        }

        public void AddLayout(Scene scene, string xmlLayout) {
            try {
                var document = XDocument.Parse(xmlLayout);
                this.VisitChildrenOf(document.Root, scene);
            } catch (Exception e) {
                throw new AssertionFailedException("Failed to add HTML layout", e);
            }
        }

        private void VisitChildrenOf(XElement element, Container parent) {
            foreach (var elementChild in element.Elements()) {

                if (elementChild is null) {
                    continue;
                }

                string elementName = elementChild.Name.LocalName;

                if (elementName == "style") {
                    CreateStyle(elementChild);
                    continue;
                }

                if (!this._elementToTypeMap.ContainsElement(elementName)) {
                    ServiceProvider.LogService?.WriteLineWarning($"Unknown node: {elementName}");
                    continue;
                }

                var attributes = new HtmlAttributes(elementChild);
                
                HtmlAttributes? style = null;
                if (attributes.TryGetValue("style-id", out string styleID)) {
                    style = this._styles[styleID];
                }
                var attributeResolver = new HtmlAttributeResolver(attributes, style);

                var newChild = this.CreateChildOf(parent, attributeResolver);
                parent.AddChild(newChild);

                if (newChild is Container c) {
                    this.VisitChildrenOf(elementChild, c);
                }
            }
        }

        private void CreateStyle(XElement elementChild) {
            var attributes = new HtmlAttributes(elementChild);

            if (!attributes.TryGetValue("id", out string id)) {
                ServiceProvider.LogService?.WriteLineWarning($"Style without id is defined");
                return;
            }

            attributes.RemoveAttribute("id");
            this._styles[id] = attributes;
        }

        private UIElement CreateUIElement(HtmlAttributeResolver attributes) {

            // Should we bind to a class?
            if (attributes.TryGetValue("class", out string className)) {
                var customType = this._customTypeResolver.Resolve(className);

                // Are we expecting to pass an id to the constructor?
                object? instance;
                if (attributes.ID != null) {
                    instance = Activator.CreateInstance(customType, attributes.ID);
                } else {
                    instance = Activator.CreateInstance(customType);
                }

                return (UIElement)instance!;
            }

            Debug.Assert(this._elementToTypeMap.ContainsElement(attributes.NodeName), $"No handler exists for {attributes.NodeName}");
            string annexTypeName = this._elementToTypeMap[attributes.NodeName];
            var annexType = this._annexTypeResolver.Resolve(annexTypeName);
            return this._uiElementActivator.CreateInstance(annexType, attributes.ID);
        }

        private UIElement CreateChildOf(Container parent, HtmlAttributeResolver attributes) {
            var child = this.CreateUIElement(attributes);

            this.SetPosition(child, parent, attributes);
            this.SetSize(child, parent, attributes);

            if (child is Image img) {
                this.SetTexture(img, attributes);
            }
            if (child is Label lbl) {
                this.SetText(lbl, attributes);
            }

            return child;
        }

        private void SetPosition(UIElement element, Container parent, HtmlAttributeResolver attributes) {
            if (!attributes.SetXYComponents("position", element.Position, parent.Size, parent.Position)) {
                element.Position.Set(parent.Position);
            }
        }

        private void SetSize(UIElement element, Container parent, HtmlAttributeResolver attributes) {
            attributes.SetXYComponents("size", element.Size, parent.Size, Vector.Create());
        }

        private void SetTexture(Image img, HtmlAttributeResolver attributes) {
            if (!attributes.TryGetValue("texture", out string texture)) {
                return;
            }
            img.ImageTextureName.Set(texture);
        }

        private void SetText(Label label, HtmlAttributeResolver attributes) {
            if (attributes.TryGetValue("text", out string text)) {
                label.Text.Set(text);
            }

            if (attributes.TryGetValue("font", out string font)) {
                label.Font.Set(font);
            } else {
                label.Font.Set("default.ttf");
            }

            if (attributes.TryGetValue("font-size", out string fontsize)) {
                label.FontSize.Set(int.Parse(fontsize));
            }

            if (attributes.TryGetValue("font-color", out string fontcolor)) {
                label.FontColor.Set(RGBA.Parse(fontcolor));
            }

            if (attributes.TryGetValue("text-alignment", out string textAlignment)) {

                var data = textAlignment.Split(',');
                string verticalAlignment = data[0].Trim().ToCamelCaseWord();
                string horizontalAlignment = data[1].Trim().ToCamelCaseWord();

                label.TextAlignment.HorizontalAlignment = (HorizontalAlignment)Enum.Parse(typeof(HorizontalAlignment), horizontalAlignment);
                label.TextAlignment.VerticalAlignment = (VerticalAlignment)Enum.Parse(typeof(VerticalAlignment), verticalAlignment);
            }
        }
    }
}
