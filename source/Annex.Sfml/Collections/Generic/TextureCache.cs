using Annex.Core.Assets;
using Annex.Core.Collections.Generic;
using SFML.Graphics;

namespace Annex.Sfml.Collections.Generic
{
    internal class TextureCache : ITextureCache
    {
        private readonly ICache<string, Texture> _cache = new Cache<string, Texture>();
        private readonly IAssetGroup _textures;
        private readonly Texture _invalidTexture = new Texture(1, 1);

        public TextureCache(IAssetService assetService) {
            this._textures = assetService.Textures();
        }

        public Texture GetTexture(string textureId) {
            if (this._cache.TryGetValue(textureId, out var cachedTexture)) {
                return cachedTexture;
            }

            var asset = this._textures.GetAsset(textureId);

            if (asset == null) {
                return _invalidTexture;
            }

            if (asset.Target is not Texture newTexture) {

                if (asset.FilepathSupported) {
                    newTexture = new Texture(asset.FilePath);
                } else {
                    newTexture = new Texture(asset.ToBytes());
                }

                asset.SetTarget(newTexture);
            }

            this._cache.Add(textureId, newTexture);
            return newTexture;
        }
    }
}
