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

            if (context.RenderPosition is not null) {
                if (sprite.Position.DoesNotEqual(context.RenderPosition)) {
                    sprite.Position = context.RenderPosition.ToSFML();
                }
            }
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