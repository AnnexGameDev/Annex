using Annex.Core.Assets;
using Annex.Core.Graphics;
using Annex.Core.Graphics.Contexts;
using SFML.Graphics;

namespace Annex.Sfml.Graphics.Windows
{
    internal abstract class SfmlCanvas : ICanvas
    {
        private readonly IAssetService _assetService;

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
            sprite.Texture = GetTexture(context.TextureId);

            this._renderTarget?.Draw(sprite);
        }

        private Texture GetTexture(string textureId) {
            var asset = this._assetService.Textures.GetAsset(textureId);

            if (asset.Target is not Texture texture) {

                if (asset.FilepathSupported) {
                    texture = new Texture(asset.FilePath);
                } else {
                    texture = new Texture(asset.ToBytes());
                }

                asset.SetTarget(texture);
            }

            return texture;
        }

        private Sprite CreateSprite(TextureContext context) {
            var sprite = new Sprite();
            context.SetPlatformTarget(sprite);
            return (context.PlatformTarget as Sprite)!;
        }
    }
}