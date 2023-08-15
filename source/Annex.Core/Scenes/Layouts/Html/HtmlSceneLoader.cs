using Annex.Core.Assets;
using Annex.Core.Data;
using Annex.Core.Graphics.Contexts;
using Annex.Core.Scenes.Elements;
using Scaffold.DependencyInjection;
using Scaffold.Extensions;
using Scaffold.Logging;
using System.Xml.Linq;

namespace Annex.Core.Scenes.Layouts.Html
{
    internal class HtmlSceneLoader : IHtmlSceneLoader
    {
        public readonly IAssetGroup _sceneDataAssets;
        private readonly IContainer _container;

        public HtmlSceneLoader(IAssetService assetService, IContainer container) {
            this._sceneDataAssets = assetService.SceneData();
            this._container = container;
        }

        public void Load(string assetId, IScene sceneInstance) {

            var document = this.GetDocumentRoot(assetId);
            var styles = new Styles(document);
            if (this.GetSceneElement(document) is not XElement scene)
            {
                Log.Trace(LogSeverity.Warning, $"Failed to retrieve scene from asset: '{assetId}'");
                return;
            }

            // Apply styles to the scene
            this.ProcessElement(sceneInstance, null, scene, styles);

            ProcessChildren(sceneInstance, scene, styles);
        }

        private void ProcessChildren(IParentElement parentInstance, XElement parentElement, Styles styles) {
            foreach (var childElement in parentElement.Elements())
            {

                if (!this.TryCreateInstance(childElement, styles, out var childInstance))
                {
                    continue;
                }

                parentInstance.AddChild(childInstance);
                this.ProcessElement(childInstance, parentInstance, childElement, styles);

                if (childInstance is IParentElement subParentInstance)
                {
                    ProcessChildren(subParentInstance, childElement, styles);
                }
            }
        }

        private bool TryCreateInstance(XElement element, Styles styles, out IUIElement uiElement) {

            string? typeToInstantiate = element.Name.ToString();
            typeToInstantiate = typeToInstantiate switch
            {
                "picture" => "image",
                "script" => null,
                _ => typeToInstantiate
            };

            if (GetStringAttribute("class", element, styles) is string className)
            {
                typeToInstantiate = className;
            }

            if (typeToInstantiate == null)
            {
                uiElement = default;
                return false;
            }

            uiElement = UIElementFactory.CreateInstance(typeToInstantiate, this._container);
            return true;
        }

        private XElement? GetSceneElement(XElement document) {
            var sceneNodes = document.Elements("scene");
            return sceneNodes.FirstOrDefault();
        }

        private XElement GetDocumentRoot(string assetId) {
            string sceneData = string.Empty;

            if (this._sceneDataAssets.GetAsset(assetId) is IAsset sceneDataAsset)
            {
                using var ms = new MemoryStream(sceneDataAsset.ToBytes());
                using var sr = new StreamReader(ms);
                sceneData = sr.ReadToEnd();
            }

            // XDocument requires a root element, so inject one manually for safety.
            string fakeRoot = $"<root>{sceneData}</root>";

            return XDocument.Parse(fakeRoot).Element("root")!;
        }


        #region Process Elements

        private void ProcessElement(IUIElement instance, IUIElement? parent, XElement element, Styles styles) {
            this.SetPosition(instance, parent, element, styles);
            this.SetSize(instance, parent, element, styles);

            this.SetElementId(instance, element, styles);

            if (instance is IImage img)
            {
                this.SetTexture(img, element, styles);
            }

            if (instance is ILabel label)
            {
                this.SetText(label, element, styles);
            }

            if (instance is IPasswordBox pb)
            {
                this.SetPasswordBox(pb, element, styles);
            }
        }

        private void SetElementId(IUIElement instance, XElement element, Styles styles) {
            if (GetStringAttribute("id", element, styles) is string elementId)
            {
                instance.ElementID = elementId;
            }
        }

        private void SetPasswordBox(IPasswordBox pb, XElement element, Styles styles) {
            if (GetStringAttribute("password-char", element, styles) is string passwordChar)
            {
                if (passwordChar.Length != 1)
                {
                    throw new InvalidOperationException($"password-char must be of length 1: {passwordChar}");
                }
                pb.PasswordChar = passwordChar[0];
            }
        }

        private void SetText(ILabel label, XElement element, Styles styles) {
            if (GetStringAttribute("text", element, styles) is string text)
            {
                label.Text = text;
            }

            if (GetStringAttribute("text-alignment", element, styles) is string alignment)
            {
                var data = alignment.Split(",");
                string horizontalAlignment = data[0].Trim().ToCamelCaseWord();
                string verticalAlignment = data[1].Trim().ToCamelCaseWord();

                label.HorizontalTextAlignment = Enum.Parse<HorizontalAlignment>(horizontalAlignment);
                label.VerticalTextAlignment = Enum.Parse<VerticalAlignment>(verticalAlignment);
            }

            if (GetStringAttribute("font", element, styles) is string font)
            {
                label.Font = font + ".ttf";
            } else
            {
                label.Font = "default.ttf";
            }

            if (GetVectorAttribute("text-offset", label.Size, element, styles) is IVector2<float> offset)
            {
                label.TextPositionOffset = new Vector2f(offset.X, offset.Y);
            }

            if (GetStringAttribute("font-size", element, styles) is string fontSize)
            {
                label.FontSize = uint.Parse(fontSize);
            }

            if (GetStringAttribute("font-color", element, styles) is string fontColor)
            {
                label.FontColor = RGBA.Parse(fontColor);
            }

            if (GetStringAttribute("border-color", element, styles) is string borderColor)
            {
                label.TextBorderColor = RGBA.Parse(borderColor);
            }

            if (GetStringAttribute("border-thickness", element, styles) is string borderThickness)
            {
                label.TextBorderThickness = float.Parse(borderThickness);
            }
        }

        private void SetTexture(IImage img, XElement element, Styles styles) {
            if (GetStringAttribute("texture", element, styles) is string textureId)
            {
                img.BackgroundTextureId = textureId;
            }

            if (GetStringAttribute("hover-texture", element, styles) is string hoverTextureId)
            {
                img.HoverBackgroundTextureId = hoverTextureId;
            }
        }

        private void SetSize(IUIElement instance, IUIElement? parent, XElement element, Styles styles) {
            if (GetVectorAttribute("size", parent?.Size, element, styles) is IVector2<float> value)
            {
                instance.Size.Set(value);
            }
        }

        private void SetPosition(IUIElement instance, IUIElement? parent, XElement element, Styles styles) {
            if (GetVectorAttribute("position", parent?.Size, element, styles) is Vector2f value)
            {
                instance.Position.Set(Vector2f.SumOf(value, parent?.Position ?? new Vector2f()));
            } else
            {
                instance.Position.Set(parent?.Position ?? new Vector2f());
            }
        }

        private string? GetStringAttribute(string attributeName, XElement element, Styles styles) {
            var elementValue = element.Attribute(attributeName)?.Value;
            var styleValue = styles.GetStyle(element.Attribute("style-id")?.Value ?? string.Empty, attributeName);
            return elementValue ?? styleValue;
        }

        private float ComputeVectorValue(string val, float parentVal) {

            val = val.Trim();

            if (val.StartsWith("calc(") && val.EndsWith(")"))
            {

                val = val[5..^1];
                var possibleOperators = new[] { '-', '+', '/', '*' };
                var terms = val.Split(possibleOperators).Select(term => ComputeVectorValue(term, parentVal)).ToList();
                var operators = val.FindAll(possibleOperators).ToList();
                operators.Insert(0, '+'); // to match the length of the terms collection

                float result = 0;
                for (int i = 0; i < operators.Count; i++)
                {
                    var op = operators[i];
                    float term = terms[i];

                    result = op switch
                    {
                        '+' => result + term,
                        '-' => result - term,
                        '*' => result * term,
                        '/' => result / term,
                        _ => throw new NotSupportedException()
                    };
                }

                return result;
            }

            return val.EndsWith("%") ? parentVal * float.Parse(val[..^1]) / 100 : float.Parse(val);
        }

        private IVector2<float>? GetVectorAttribute(string attributeName, IVector2<float>? parentValue, XElement element, Styles styles) {
            var finalValue = GetStringAttribute(attributeName, element, styles);
            if (finalValue == null)
            {
                return null;
            }

            var data = finalValue.Split(',').Select(val => val.Trim()).ToArray();
            string x = data[0];
            string y = data[1];


            float xf = ComputeVectorValue(x, parentValue?.X ?? 0);
            float yf = ComputeVectorValue(y, parentValue?.Y ?? 0);
            return new Vector2f(xf, yf);
        }

        #endregion

        private class Styles
        {
            private readonly Dictionary<string, IDictionary<string, string>> _styles = new Dictionary<string, IDictionary<string, string>>();

            public Styles(XElement document) {
                var styleElements = document.Elements("style");

                foreach (var styleElement in styleElements)
                {

                    string id = styleElement.Attribute("id")!.Value;
                    var style = new Dictionary<string, string>();
                    this._styles[id] = style;

                    foreach (var attribute in styleElement.Attributes().Where(attribute => attribute.Name != "id"))
                    {
                        style[attribute.Name.ToString()] = attribute.Value;
                    }
                }
            }

            public string? GetStyle(string styleId, string property) {
                if (!this._styles.ContainsKey(styleId))
                {
                    return null;
                }

                var style = this._styles[styleId];
                if (!style.ContainsKey(property))
                {
                    return null;
                }

                return style[property];
            }
        }
    }
}
