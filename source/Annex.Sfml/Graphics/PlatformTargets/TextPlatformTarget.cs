﻿using Annex.Core.Assets;
using Annex.Core.Data;
using Annex.Core.Graphics.Contexts;
using Annex.Sfml.Extensions;
using SFML.Graphics;
using Vector2f = SFML.System.Vector2f;

namespace Annex.Sfml.Graphics.PlatformTargets
{
    internal class TextPlatformTarget : PlatformTarget
    {
        private readonly Text _text;
        private readonly TextContext _textContext;
        private readonly IAssetService _assetService;


        public TextPlatformTarget(TextContext textContext, IAssetService assetService) {
            this._textContext = textContext;
            this._assetService = assetService;
            this._text = new();
        }

        public override void Dispose() {
            this._text.Dispose();
        }

        protected override void Draw(RenderTarget renderTarget) {
            this.UpdateIfNeeded();
            renderTarget.Draw(this._text);
        }

        private void UpdateIfNeeded() {
            var font = UpdateFont(this._textContext.Font.Value);
            var text = UpdateText(this._textContext.Text.Value);
            var fontSize = UpdateFontSize(this._textContext.FontSize);
            var color = UpdateFontColor(this._textContext.Color);
            var borderThickness = UpdateBorderThickness(this._textContext.BorderThickness);
            var borderColor = UpdateBorderColor(this._textContext.BorderColor);

            var position = UpdatePosition(this._textContext.Position);
            var origin = UpdateOrigin(this._textContext.HorizontalAlignment, this._textContext.VerticalAlignment);
            var rotation = UpdateRotation(this._textContext.Rotation);
        }

        private float UpdateRotation(Shared<float>? rotation) {
            const float DefaultRotation = 0;
            var finalRotation = rotation?.Value ?? DefaultRotation;
            if (this._text.Rotation != finalRotation) {
                this._text.Rotation = finalRotation;
            }
            return this._text.Rotation;
        }

        private Vector2f UpdateOrigin(HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment) {
            var bounds = this._text.GetLocalBounds();
            Vector2f desiredOrigin = new Vector2f(
                horizontalAlignment.Align(bounds),
                verticalAlignment.Align(bounds)
            );

            if (this._text.Origin != desiredOrigin) {
                this._text.Origin = desiredOrigin;
            }
            return this._text.Origin;
        }

        private Vector2f UpdatePosition(IVector2<float>? position) {
            if (this._text.Position.DoesNotEqual(position)) {
                this._text.Position = position.ToSFML();
            }
            return this._text.Position;
        }

        private Color UpdateBorderColor(RGBA? borderColor) {
            if (this._text.OutlineColor.DoesNotEqual(borderColor, Color.Black)) {
                this._text.OutlineColor = borderColor.ToSFML(KnownColor.Black);
            }
            return this._text.OutlineColor;
        }

        private float UpdateBorderThickness(Shared<float>? borderThickness) {
            const float DefaultBorderThickness = 0;
            float finalBorderThickness = borderThickness?.Value ?? DefaultBorderThickness;
            if (this._text.OutlineThickness != finalBorderThickness) {
                this._text.OutlineThickness = finalBorderThickness;
            }
            return this._text.OutlineThickness;
        }

        private Color UpdateFontColor(RGBA? color) {
            if (this._text.FillColor.DoesNotEqual(color, Color.Black)) {
                this._text.FillColor = color.ToSFML(KnownColor.Black);
            }
            return this._text.FillColor;
        }

        private uint UpdateFontSize(Shared<uint>? fontSize) {
            const uint DefaultFontSize = 12;
            uint finalFontSize = fontSize?.Value ?? DefaultFontSize;
            if (this._text.CharacterSize != finalFontSize) {
                this._text.CharacterSize = finalFontSize;
            }
            return this._text.CharacterSize;
        }

        private string UpdateText(string text) {
            if (this._text.DisplayedString != text) {
                this._text.DisplayedString = text;
            }
            return this._text.DisplayedString;
        }

        private Font UpdateFont(string font) {
            var sfmlFont = GetFont(font);
            if (sfmlFont != this._text.Font) {
                this._text.Font = sfmlFont;
            }
            return this._text.Font;
        }

        private Font GetFont(string fontId) {
            var asset = this._assetService.Fonts.GetAsset(fontId);

            if (asset is not Font font) {
                if (asset.FilepathSupported) {
                    font = new Font(asset.FilePath);
                } else {
                    font = new Font(asset.ToBytes());
                }
            }

            return font;
        }
    }
}
