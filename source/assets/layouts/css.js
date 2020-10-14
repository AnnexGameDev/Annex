var head = document.getElementsByTagName('head')[0];
var link = document.createElement('link');
link.setAttribute('rel', 'stylesheet');
link.setAttribute('type', 'text/css');
link.setAttribute('href', 'style.css');
head.appendChild(link);

var textureFolder = "../textures/";

function ApplyPosition(e) {
	var x = "0px";
	var y = "0px";
	var positions = e.getAttribute("position");
	if (positions != undefined) {
		positions = positions.split(',');
		x = positions[0].trim();
		y = positions[1].trim();
		
		if (x.slice(-1) != "%") {
			x = x + "px";
		}
		if (y.slice(-1) != "%") {
			y = y + "px";
		}
	}
	e.style.top = y;
	e.style.left = x;
}

function ApplySize(e) {
	var sizes = e.getAttribute("size");
	if (sizes != undefined) {
		sizes = sizes.split(',');
		var x = sizes[0].trim();
		var y = sizes[1].trim();

		if (x.slice(-1) != "%") {
			x = x + "px";
		}
		if (y.slice(-1) != "%") {
			y = y + "px";
		}

		e.style.height = y;
		e.style.width = x;
	}
}

function ApplyText(e) {
	var text = e.getAttribute("text");
	var child = document.createElement("span");
	child.style.display = "flex";
	if (text != undefined) {
		child.innerHTML = text;
		e.appendChild(child);
	}

	var alignments = e.getAttribute("text-alignment");
	if (alignments != undefined) {
		alignments = alignments.split(',');
		for (var i = 0; i < alignments.length; i++) {
			var alignment = alignments[i].trim();

			if (alignment == "left") {
				child.style.marginRight = "auto";
			}
			if (alignment == "right") {
				child.style.marginLeft = "auto";
			}
			if (alignment == "center") {
				child.style.margin = "0 auto";
			}
			if (alignment == "top") {
				child.style.alignSelf = "start";
			}
			if (alignment == "middle") {
				child.style.alignSelf = "center";
			}
			if (alignment == "bottom") {
				child.style.alignSelf = "end";
			}
		}
	}
}

function ApplyTexture(e) {
	var path = e.getAttribute('texture');
	e.style.background = "none";
	if (path != undefined) {
		var url = "url('" + textureFolder + path + "')";
		e.style.backgroundImage = url;
		e.style.backgroundColor = "transparent";
		e.style.backgroundSize = "cover";
	}
}

function ApplyFont(e) {
	var font = e.getAttribute('font');
	if (font != undefined) {
		if (font == "default") {
			font = "scene default";
		}

		e.style.fontFamily = font;
	}

	var fontsize = e.getAttribute('font-size');
	if (fontsize != undefined) {
		e.style.fontSize = fontsize + "px";
	}

	var fontcolor = e.getAttribute('font-color');
	if (fontcolor != undefined) {
		if (fontcolor.split(',').length != 1) {
			fontcolor = "rgb(" + fontcolor + ")";
		}
		e.style.color = fontcolor;
	}
}

function ApplyTag(tag) {
	var elements = document.getElementsByTagName(tag);
	for (var i = 0; i < elements.length; i++) {
		var element = elements[i];
		ApplyPosition(element);
		ApplySize(element);
		ApplyTexture(element);
		ApplyText(element);
		ApplyFont(element);
	}
}

var tags = ["container", "picture", "label", "textbox", "button"];
tags.forEach(tag => ApplyTag(tag));