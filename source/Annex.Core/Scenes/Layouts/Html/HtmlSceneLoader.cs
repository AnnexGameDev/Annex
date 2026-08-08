using Annex.Core.Assets;
using Annex.Core.Data;
using Annex.Core.Graphics.Contexts;
using Annex.Core.Scenes.Elements;
using Scaffold.DependencyInjection;
using Scaffold.Logging;
using System.Xml.Linq;

namespace Annex.Core.Scenes.Layouts.Html;

internal class HtmlSceneLoader : IHtmlSceneLoader
{
    private readonly IContainer _container;
    private readonly IUIElementTypeResolverService _uiElementTypeResolverService;

    public HtmlSceneLoader(IContainer container, IUIElementTypeResolverService uIElementTypeResolverService)
    {
        _container = container;
        _uiElementTypeResolverService = uIElementTypeResolverService;
    }

    public void Load(string assetId, IScene sceneInstance)
    {
        var htmlScenes = sceneInstance.Assets.GetHtmlScenes();

        var document = GetDocumentRoot(assetId, htmlScenes);
        var styles = new Styles(document);
        if (GetSceneElement(document) is not XElement scene)
        {
            Log.Warning($"Failed to retrieve scene from asset: '{assetId}'");
            return;
        }

        // Apply styles to the scene, most other properties are already applied
        SetElementId(sceneInstance, scene, styles);

        ProcessChildren(sceneInstance, scene, styles, sceneInstance.GetType());
    }

    public void RefreshUI(IScene scene)
    {
        RefreshUI(scene, scene.Children);
    }

    private void RefreshUI(IUIElement parent, IEnumerable<IUIElement> children)
    {
        foreach (var child in children)
        {
            if (child.Size is HtmlSceneVector2f size)
            {
                size.Refresh(parent.Size, null);
            }
            if (child.Position is HtmlSceneVector2f position)
            {
                position.Refresh(parent.Size, parent.Position);
            }

            if (child is IParentElement subParentInstance)
            {
                RefreshUI(subParentInstance, subParentInstance.Children);
            }
        }
    }

    private void ProcessChildren(IAddableParentElement parentInstance, XElement parentElement, Styles styles, Type sceneType)
    {
        foreach (var childElement in parentElement.Elements())
        {
            if (!TryCreateInstance(parentInstance, childElement, styles, sceneType, out var childInstance))
            {
                continue;
            }

            parentInstance.AddChild(childInstance);
            ProcessElement(childInstance, parentInstance, childElement, styles);

            if (childInstance is IAddableParentElement subParentInstance)
            {
                ProcessChildren(subParentInstance, childElement, styles, sceneType);
            }
        }
    }

    private bool TryCreateInstance(IUIElement parent, XElement element, Styles styles, Type sceneType, out IUIElement uiElement)
    {
        string? typeNameToInstantiate = element.Name.ToString();
        typeNameToInstantiate = typeNameToInstantiate switch
        {
            "picture" => "image",
            "script" => null,
            _ => typeNameToInstantiate
        };

        if (GetStringAttribute("class", element, styles) is string className && !string.IsNullOrWhiteSpace(className))
        {
            typeNameToInstantiate = className;
        }

        if (typeNameToInstantiate == null)
        {
            uiElement = default;
            return false;
        }

        var position = GetPosition(parent, element, styles);
        var size = GetSize(parent, element, styles);
        var args = new UIElementCreationArgs(position: position, size: size);
        using var containerScope = _container.CreateScope();
        containerScope.Register<UIElementCreationArgs?>(() => args);

        if (_uiElementTypeResolverService.ResolveType(typeNameToInstantiate, sceneType) is Type type)
        {
            uiElement = containerScope.Resolve(type) as IUIElement;
            return true;
        }
        uiElement = default;
        return false;
    }

    private XElement? GetSceneElement(XElement document)
    {
        var sceneNodes = document.Elements("scene");
        return sceneNodes.FirstOrDefault();
    }

    private XElement GetDocumentRoot(string assetId, IAssetStore htmlScenes)
    {
        string sceneData = (string)htmlScenes.GetUntyped(assetId);

        // XDocument requires a root element, so inject one manually for safety.
        string fakeRoot = $"<root>{sceneData}</root>";

        return XDocument.Parse(fakeRoot).Element("root")!;
    }


    #region Process Elements

    private void ProcessElement(IUIElement instance, IUIElement? parent, XElement element, Styles styles)
    {
        SetElementId(instance, element, styles);

        if (instance is IImage img)
        {
            SetTexture(img, element, styles);
        }

        if (instance is ILabel label)
        {
            SetText(label, element, styles);
        }

        if (instance is IPasswordBox pb)
        {
            SetPasswordBox(pb, element, styles);
        }

        if (instance is ListView lv)
        {
            SetListView(lv, element, styles);
        }
    }

    private void SetElementId(IUIElement instance, XElement element, Styles styles)
    {
        if (GetStringAttribute("id", element, styles) is string elementId)
        {
            instance.ElementID = elementId;
        }
    }

    private void SetListView(ListView listview, XElement element, Styles styles)
    {
        if (GetIntAttribute("line-height", element, styles) is int lineHeight)
        {
            listview.LineHeight = lineHeight;
        }

        if (GetStringAttribute("hover-item-texture", element, styles) is string hoverItemTexture)
        {
            listview.HoverItemTextureId = hoverItemTexture;
        }

        if (GetStringAttribute("selected-item-texture", element, styles) is string selectedItemTexture)
        {
            listview.SelectedItemTextureId = selectedItemTexture;
        }

        if (GetStringAttribute("font-color", element, styles) is string fontColor)
        {
            listview.FontColor = RGBA.Parse(fontColor);
        }

        if (GetStringAttribute("selected-font-color", element, styles) is string selectedFontColor)
        {
            listview.SelectedFontColor = RGBA.Parse(selectedFontColor);
        }

        if (GetStringAttribute("font-size", element, styles) is string fontSize)
        {
            listview.FontSize = uint.Parse(fontSize);
        }

        if (GetBoolAttribute("selectable", element, styles) is bool selectable)
        {
            listview.IsSelectable = selectable;
        }

        if (GetBoolAttribute("show-index-prefix", element, styles) is bool showIndexPrefix)
        {
            listview.ShowIndexPrefix = showIndexPrefix;
        }
    }

    private void SetPasswordBox(IPasswordBox pb, XElement element, Styles styles)
    {
        if (GetStringAttribute("password-char", element, styles) is string passwordChar)
        {
            if (passwordChar.Length != 1)
            {
                throw new InvalidOperationException($"password-char must be of length 1: {passwordChar}");
            }
            pb.PasswordChar = passwordChar[0];
        }
    }

    private void SetText(ILabel label, XElement element, Styles styles)
    {
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
        }
        else
        {
            label.Font = "default.ttf";
        }

        if (GetVectorAttribute("text-offset", label.Size, null, element, styles) is IVector2<float> offset)
        {
            label.TextPositionOffset = offset;
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

    private void SetTexture(IImage img, XElement element, Styles styles)
    {
        if (GetStringAttribute("texture", element, styles) is string textureId)
        {
            img.BackgroundTextureId = textureId;
        }

        if (GetStringAttribute("hover-texture", element, styles) is string hoverTextureId)
        {
            img.HoverBackgroundTextureId = hoverTextureId;
        }

        if (GetStringAttribute("focused-texture", element, styles) is string focusedTextureId)
        {
            img.FocusedBackgroundTextureId = focusedTextureId;
        }
    }

    private IVector2<float>? GetSize(IUIElement? parent, XElement element, Styles styles)
    {
        if (GetVectorAttribute("size", parent?.Size, null, element, styles) is IVector2<float> value)
        {
            return value;
        }
        return null;
    }

    private IVector2<float>? GetPosition(IUIElement? parent, XElement element, Styles styles)
    {
        if (GetVectorAttribute("position", parent?.Size, parent?.Position, element, styles) is IVector2<float> value)
        {
            return value;
        }
        return new HtmlSceneVector2f("0, 0", parent?.Size, parent?.Position);
    }

    private string? GetStringAttribute(string attributeName, XElement element, Styles styles)
    {
        var elementValue = element.Attribute(attributeName)?.Value;
        var styleValue = styles.GetStyle(element.Attribute("style-id")?.Value ?? string.Empty, attributeName);
        return elementValue ?? styleValue;
    }

    private int? GetIntAttribute(string attributeName, XElement element, Styles styles)
    {
        if (GetStringAttribute(attributeName, element, styles) is string strAttribute)
        {
            if (int.TryParse(strAttribute, out var intAttribute))
            {
                return intAttribute;
            }
            else
            {
                Log.Warning($"Error while parsing attribute {attributeName}:{strAttribute} to int");
                return null;
            }
        }
        return null;
    }

    private bool? GetBoolAttribute(string attributeName, XElement element, Styles styles)
    {
        if (GetStringAttribute(attributeName, element, styles) is string strAttribute)
        {
            if (bool.TryParse(strAttribute, out var boolAttribute))
            {
                return boolAttribute;
            }
            else
            {
                Log.Warning($"Error while parsing attribute {attributeName}:{strAttribute} to bool");
                return null;
            }
        }
        return null;
    }

    private IVector2<float>? GetVectorAttribute(string attributeName, IVector2<float>? parentValue, IVector2<float>? offset, XElement element, Styles styles)
    {
        var finalValue = GetStringAttribute(attributeName, element, styles);
        if (finalValue == null)
        {
            return null;
        }

        return new HtmlSceneVector2f(finalValue, parentValue, offset);
    }

    #endregion

    private class Styles
    {
        private readonly Dictionary<string, IDictionary<string, string>> _styles = new Dictionary<string, IDictionary<string, string>>();

        public Styles(XElement document)
        {
            var styleElements = document.Elements("style");

            foreach (var styleElement in styleElements)
            {

                string id = styleElement.Attribute("id")!.Value;
                var style = new Dictionary<string, string>();
                _styles[id] = style;

                foreach (var attribute in styleElement.Attributes().Where(attribute => attribute.Name != "id"))
                {
                    style[attribute.Name.ToString()] = attribute.Value;
                }
            }
        }

        public string? GetStyle(string styleId, string property)
        {
            if (!_styles.ContainsKey(styleId))
            {
                return null;
            }

            var style = _styles[styleId];
            if (!style.ContainsKey(property))
            {
                return null;
            }

            return style[property];
        }
    }
}