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

function ProcessNumberValue(value) {

    if (value == undefined) {
        return undefined;
    }

    value = value.trim();

    if (value.startsWith("calc(") && value.endsWith(")")) {

        value = value.slice(5, value.length - 1);

        var possibleOperatorsRegex = /[-+]/g;
        var values = value.split(possibleOperatorsRegex);
        var operators = [...value.matchAll(possibleOperatorsRegex)];

        // Rejoin them
        var result = "";
        for (var i = 0; i < operators.length; i++) {
            result = result + ProcessNumberValue(values[i]) + " " + operators[i] + " ";
        }
        result = result + ProcessNumberValue(values[operators.length]);
        
        return "calc(" + result + ")";
    }

    if (value.slice(-1) != "%") {
        value = value + "px";
    }

    return value;
}

function GetXY(e, style, propertyName) {
    var propertyValue = Get(e, style, propertyName);
    if (propertyValue == undefined)
        return undefined;

    var values = propertyValue.split(',');
    var x = ProcessNumberValue(values[0]);
    var y = ProcessNumberValue(values[1]);

    return [x, y];
}

function Add(style, e, property) {
    style[property] = CouldExist(e.getAttribute(property));
}

function ApplyPosition(e, style) {
    var positions = GetXY(e, style, "position");

    if (positions == undefined)
        return;

    e.style.left = positions[0];
    e.style.top = positions[1];
}

function ApplySize(e, style) {
    var sizes = GetXY(e, style, "size");

    if (sizes == undefined)
        return;

    e.style.width = sizes[0];
    e.style.height = sizes[1];
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

function ApplyText(e, style) {
    var text = Get(e, style, "text");

    if (text == undefined)
        return;

    var font = Get(e, style, "font");
    if (font != undefined) {
        if (font == "default")
            font = "scene default";
    
        e.style.fontFamily = font;
    }

    var fontSize = Get(e, style, "font-size");
    if (fontSize != undefined) {
        e.style.fontSize = fontSize + "px";
    }

    var fontColor = Get(e, style, "font-color");
    if (fontColor != undefined) {
        if (fontColor.split(',').length != 1) {
            fontColor = "rgb(" + fontColor + ")";
        }
        e.style.color = fontColor;
    }

    var te = document.createElement("text");
    te.innerHTML = text;
    e.appendChild(te);

    var canvas = document.createElement("canvas");
    var context = canvas.getContext("2d");
    context.font = fontSize + "px " + font;
    var metrics = context.measureText(text);
    
    var offsets = GetXY(e, style, "text-offset");
    var offsetX = "0px";
    var offsetY = "0px";

    if (offsets != undefined) {
        offsetX = offsets[0];
        offsetY = offsets[1];
        
        var parentHeight = window.getComputedStyle(e).height;
        var parentWidth = window.getComputedStyle(e).width;

        if (offsetX.endsWith("%")) {
            offsetX = parentWidth + " * " + offsetX.slice(0, -1) + " / 100";
        }

        if (offsetY.endsWith("%")) {
            offsetY = parentHeight + " * " + offsetY.slice(0, -1) + " / 100";
        }
    }

    var textWidth = window.getComputedStyle(te).width;
    var textHeight = window.getComputedStyle(te).height;

    var alignment = Get(e, style, "text-alignment");
    if (alignment != undefined) {
        var alignments = alignment.split(',');
        var halignment = alignments[0].trim();
        var valignment = alignments[1].trim();
    
        if (halignment == "left") {
        }
        if (halignment == "right") {
            offsetX = offsetX + " - " + textWidth;
            // do nothing
        }
        if (halignment == "center") {
            offsetX = offsetX + " - " + textWidth + " / 2";
        }
        if (valignment == "top") {
            // do nothing
        }
        if (valignment == "middle") {
            offsetY = offsetY + " - " + textHeight + " / 2";
        }
        if (valignment == "bottom") {
            offsetY = offsetY + " - " + textHeight;
        }
    }
    te.style.left = "calc(" + offsetX + ")";
    te.style.top = "calc(" + offsetY + ")";
}

function ProcessTag(tag) {
    var elements = document.getElementsByTagName(tag);
    for (var i = 0; i < elements.length; i++) {
        var element = elements[i];
        var styleId = element.getAttribute("style-id")
        var style = styles[styleId];

        ApplyPosition(element, style);
        ApplySize(element, style);
        ApplyTexture(element, style);
        ApplyText(element, style);
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
        var stylesToUse = [
            "size",
            "texture",
            "text",
            "font-size",
            "font",
            "text-alignment",
            "font-color",
            "position",
            "text-offset",
        ];
        for (var ii = 0; ii < stylesToUse.length; ii++) {
            Add(style, styleElement, stylesToUse[ii]);
        }

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
var tagsToFormat = ["scene", "container", "picture", "label", "button", "textbox", "passwordbox"]

// Delay by 50ms to make things easy
setTimeout(function(e) {
    tagsToFormat.forEach(tag => ProcessTag(tag));
}, 50);