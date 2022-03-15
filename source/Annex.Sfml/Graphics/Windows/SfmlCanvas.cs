using Annex.Core.Assets;
using Annex.Core.Collections.Generic;
using Annex.Core.Graphics;
using Annex.Core.Graphics.Contexts;
using Annex.Sfml.Extensions;
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

        public void Draw(SpriteContext context) {
        }

        public void Draw(TextureContext context) {

            if (context.PlatformTarget is not Sprite sprite) {
                sprite = CreateSprite(context);
            }
            UpdateSpriteIfNeeded(sprite, context);

            this._renderTarget?.Draw(sprite);
        }

        private void UpdateSpriteIfNeeded(Sprite sprite, TextureContext context) {
            var texture = this.GetTexture(context.TextureId);
            if (texture != sprite.Texture) {
                sprite.Texture = texture;
            }

            if (sprite.Position.DoesNotEqual(context.RenderPosition)) {
                sprite.Position = context.RenderPosition.ToSFML();
            }

            if (sprite.TextureRect.DoesNotEqual(context.SourceTextureRect)) {
                if (RectIsNotDefault(context.SourceTextureRect, texture.Size)) {
                    sprite.TextureRect = context.SourceTextureRect.ToSFML();
                }
            }

            // Compute scale
            var scale = ComputeSpriteScale(context, texture);
            if (sprite.Scale != scale) {
                sprite.Scale = scale;
            }
        }

        private bool RectIsNotDefault(Core.Data.IntRect? sourceTextureRect, SFML.System.Vector2u size) {
            if (sourceTextureRect == null)
                return false;
            return sourceTextureRect.Top != 0 || sourceTextureRect.Left != 0 
                || sourceTextureRect.Width != size.X || sourceTextureRect.Height != size.Y;
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

        private Sprite CreateSprite(TextureContext context) {
            var sprite = new Sprite();
            context.SetPlatformTarget(sprite);
            return (context.PlatformTarget as Sprite)!;
        }
    }
}