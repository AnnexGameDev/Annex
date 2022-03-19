using Annex.Core.Assets;
using Annex.Core.Calculations;
using Annex.Core.Collections.Generic;
using Annex.Core.Data;
using Annex.Core.Graphics;
using Annex.Core.Graphics.Contexts;
using Annex.Sfml.Extensions;
using Annex.Sfml.Graphics.Transforms;
using SFML.Graphics;

namespace Annex.Sfml.Graphics.Windows
{
    internal abstract class SfmlCanvas : ICanvas
    {
        private readonly IAssetService _assetService;
        private readonly ICache<string, Texture> _textureCache = new Cache<string, Texture>();

        protected abstract RenderTarget? _renderTarget { get; }

        public SfmlCanvas(IAssetService assetService) {
            this._assetService = assetService;
        }

        public void Draw(SpritesheetContext context) {
        }

        public void Draw(TextureContext context) {
            if (context.PlatformTarget is not SpriteContext sprite) {
                sprite = new SpriteContext(context, this._textureCache);
                context.SetPlatformTarget(sprite);
            }
            sprite.Draw(this._renderTarget);
        }

        private void UpdateSpriteIfNeeded(Sprite sprite, TextureContext context) {
            var texture = this.GetTexture(context.TextureId.Value);
            if (texture != sprite.Texture) {
                sprite.Texture = texture;
            }

            if (sprite.Position.DoesNotEqual(context.RenderPosition)) {
                sprite.Position = context.RenderPosition.ToSFML();
            }

            if (sprite.TextureRect.DoesNotEqual(context.SourceTextureRect, 0, 0, (int)texture.Size.X, (int)texture.Size.Y)) {
                sprite.TextureRect = context.SourceTextureRect.ToSFML();
            }

            // Compute scale
            var scale = ComputeSpriteScale(context, texture);
            if (sprite.Scale != scale) {
                sprite.Scale = scale;
            }

            if (sprite.Color.DoesNotEqual(context.RenderColor, Color.White)) {
                sprite.Color = context.RenderColor.ToSFML(KnownColor.White);
            }

            // SFML rotates clockwise. We should stick to math. Convert from counter-clockwise to clockwise
            float sfmlRotation = (360 - (context.Rotation?.Value ?? 0)) % 360;

            if (sprite.Rotation != sfmlRotation) {

                if (context.RelativeRotationOrigin != null) {
                    (var x, var y) = Rotation.ComputeUnits((context.Rotation.Value % 360 - 90).ToRadians());

                    var offset = new SFML.System.Vector2f(context.RelativeRotationOrigin.Y, context.RelativeRotationOrigin.X);
                    sprite.Position += offset;
                }
                sprite.Rotation = sfmlRotation;
            }
        }

        private SFML.System.Vector2f ComputeSpriteScale(TextureContext context, Texture texture) {
            float desiredRenderX = context.RenderSize?.X ?? texture.Size.X;
            float desiredRenderY = context.RenderSize?.Y ?? texture.Size.Y;

            if (context.SourceTextureRect is not null) {
                return new SFML.System.Vector2f(
                    desiredRenderX / context.SourceTextureRect.Width,
                    desiredRenderY / context.SourceTextureRect.Height
                );
            }

            return new SFML.System.Vector2f(
                desiredRenderX / texture.Size.X,
                desiredRenderY / texture.Size.Y
            );
        }

        private Texture GetTexture(string textureId) {

            if (this._textureCache.TryGetValue(textureId, out var cachedTexture)) {
                return cachedTexture;
            }

            var asset = this._assetService.Textures.GetAsset(textureId);

            if (asset.Target is not Texture newTexture) {

                if (asset.FilepathSupported) {
                    newTexture = new Texture(asset.FilePath);
                } else {
                    newTexture = new Texture(asset.ToBytes());
                }

                asset.SetTarget(newTexture);
            }

            this._textureCache.Add(textureId, newTexture);
            return newTexture;
        }
    }
}