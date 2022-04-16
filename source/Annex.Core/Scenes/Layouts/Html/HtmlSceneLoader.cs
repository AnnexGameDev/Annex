using Annex.Core.Assets;
using Annex.Core.Data;
using Annex.Core.Scenes.Components;
using System.Xml.Linq;

namespace Annex.Core.Scenes.Layouts.Html
{
    internal class HtmlSceneLoader : IHtmlSceneLoader
    {
        public readonly IAssetGroup _sceneDataAssets;

        public HtmlSceneLoader(IAssetService assetService) {
            this._sceneDataAssets = assetService.SceneData;
        }

        public void Load(string assetId, IScene sceneInstance) {
            
            var document = this.GetDocumentRoot(assetId);
            var styles = new Styles(document);
            var scene = this.GetSceneElement(document);

            // Apply styles to the scene
            this.ProcessElement(sceneInstance, null, scene, styles);

            ProcessChildren(sceneInstance, scene, styles);
        }

        private void ProcessChildren(IParentElement parentInstance, XElement parentElement, Styles styles) {
            foreach (var childElement in parentElement.Elements()) {

                if (!this.TryCreateInstance(childElement.Name.ToString(), out var childInstance)) {
                    continue;
                }

                parentInstance.AddChild(childInstance);
                this.ProcessElement(childInstance, parentInstance, childElement, styles);

                if (childInstance is IParentElement subParentInstance) {
                    ProcessChildren(subParentInstance, childElement, styles);
                }
            }
        }

        private bool TryCreateInstance(string elementName, out IUIElement uiElement) {

            string? modifiedName = elementName switch {
                "picture" => "image",
                "script" => null,
                _ => elementName
            };

            if (modifiedName == null) {
                uiElement = default;
                return false;
            }

            uiElement = UIElementFactory.CreateInstance(modifiedName);
            return true;
        }

        private XElement GetSceneElement(XElement document) {
            var sceneNodes = document.Elements("scene");
            return sceneNodes.Single();
        }

        private XElement GetDocumentRoot(string assetId) {
            var sceneDataAsset = this._sceneDataAssets.GetAsset(assetId)!;
            using var ms = new MemoryStream(sceneDataAsset.ToBytes());
            using var sr = new StreamReader(ms);
            string sceneData = sr.ReadToEnd();

            // XDocument requires a root element, so inject one manually for safety.
            string fakeRoot = $"<root>{sceneData}</root>";

            return XDocument.Parse(fakeRoot).Element("root")!;
        }


        #region Process Elements

        private void ProcessElement(IUIElement instance, IUIElement? parent, XElement element, Styles styles) {
            this.SetPosition(instance, parent, element, styles);
            this.SetSize(instance, parent, element, styles);

            if (instance is Image img) {
                this.SetTexture(img, element, styles);
            }
        }

        private void SetTexture(Image img, XElement element, Styles styles) {
            if (GetStringAttribute("texture", element, styles) is string textureId) {
                img.BackgroundTextureId = textureId;
            }
        }

        private void SetSize(IUIElement instance, IUIElement? parent, XElement element, Styles styles) {
            if (GetVectorAttribute("size", parent?.Size, element, styles) is IVector2<float> value) {
                instance.Size.Set(value);
            }
        }

        private void SetPosition(IUIElement instance, IUIElement? parent, XElement element, Styles styles) {
            if (GetVectorAttribute("position", parent?.Position, element, styles) is IVector2<float> value) {
                instance.Position.Set(value);
            }
        }

        private string? GetStringAttribute(string attributeName, XElement element, Styles styles) {
            var elementValue = element.Attribute(attributeName)?.Value;
            var styleValue = styles.GetStyle(element.Attribute("style-id")?.Value ?? string.Empty, attributeName);
            return elementValue ?? styleValue;
        }

        private IVector2<float>? GetVectorAttribute(string attributeName, IVector2<float>? parentvalue, XElement element, Styles styles) {
            var finalValue = GetStringAttribute(attributeName, element, styles);
            if (finalValue == null) {
                return null;
            }

            float?[] data = finalValue.Split(',')
                .Select(val => val.Trim())
                .Select(val => val.EndsWith("%") ? (float?)null : float.Parse(val))
                .ToArray();

            data[0] ??= parentvalue?.X;
            data[1] ??= parentvalue?.Y;

            if (data.Any(val => val == null)) {
                throw new InvalidOperationException($"% isn't supported for the property {attributeName} for an element {element.Name}");
            }

            return new Vector2f((float)data[0]!, (float)data[1]!);
        }

        #endregion

        private class Styles
        {
            private readonly Dictionary<string, IDictionary<string, string>> _styles = new Dictionary<string, IDictionary<string, string>>();

            public Styles(XElement document) {
                var styleElements = document.Elements("style");

                foreach (var styleElement in styleElements) {

                    string id = styleElement.Attribute("id")!.Value;
                    var style = new Dictionary<string, string>();
                    this._styles[id] = style;

                    foreach (var attribute in styleElement.Attributes().Where(attribute => attribute.Name != "id")) {
                        style[attribute.Name.ToString()] = attribute.Value;
                    }
                }
            }

            public string? GetStyle(string styleId, string property) {
                if (!this._styles.ContainsKey(styleId)) {
                    return null;
                }

                var style = this._styles[styleId];
                if (!style.ContainsKey(property)) {
                    return null;
                }

                return style[property];
            }
        }
    }
}
