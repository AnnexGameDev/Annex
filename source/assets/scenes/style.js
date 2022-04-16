var textureFolder = "../textures/";


function ShouldExist(value) {
    if (value == undefined)
        throw "Value is undefined!";
    return value;
}

function CouldExist(value, defaultValue = undefined) {
    if (value == undefined)
        return defaultValue;
    return value;
}

function Get(e, style, property) {
    var e_value = e.getAttribute(property)
    if (e_value != undefined) 
        return e_value;

    if (style == undefined)
        return undefined;

    var style_value = style[property]
    if (style_value != undefined)
        return style_value;

    return undefined;
}

function Add(style, e, property) {
    style[property] = CouldExist(e.getAttribute(property));
}

function ApplySize(e, style) {
    var size = Get(e, style, "size");
    if (size == undefined)
        return;

    var sizes = size.split(',');
    var x = sizes[0].trim();
    var y = sizes[1].trim();

    if (x.slice(-1) != "%") {
        x = x + "px";
    }
    if (y.slice(-1) != "%") {
        y = y + "px";
    }

    e.style.width = x;
    e.style.height = y;
}

function ApplyTexture(e, style) {
    var texture = Get(e, style, "texture");
    e.style.background = "none";

    if (texture != undefined) {
        e.style.backgroundImage = "url('" + textureFolder + texture + "')";
        e.style.backgroundColor = "transparent";
        e.style.backgroundSize = "100% 100%";
    }
}

function ProcessTag(tag) {
    var elements = document.getElementsByTagName(tag);
    for (var i = 0; i < elements.length; i++) {
        var element = elements[i];
        var styleId = element.getAttribute("style-id")
        var style = styles[styleId];

        ApplySize(element, style);
        ApplyTexture(element, style);
    }
}

function CreateStyles() {
    var styles = {};
    var styleElements = document.getElementsByTagName("style");
    for (var i = 0; i < styleElements.length; i++) {
        var styleElement = styleElements[i];

        var id = ShouldExist(styleElement.getAttribute("id"));
        var style = {}

        // Add style properties here
        Add(style, styleElement, "size");
        Add(style, styleElement, "texture");

        styles[id] = style;
    }

    return styles;
}

// Add css to the file
var head = document.getElementsByTagName('head')[0];
var link = document.createElement('link');
link.setAttribute('rel', 'stylesheet');
link.setAttribute('type', 'text/css');
link.setAttribute('href', 'style.css');
head.appendChild(link);

var styles = CreateStyles();
var tagsToFormat = ["scene", "picture"]
tagsToFormat.forEach(tag => ProcessTag(tag))