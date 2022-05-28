using Annex_Old.Core.Assets;
using Annex_Old.Core.Collections.Generic;
using SFML.Graphics;

namespace Annex_Old.Sfml.Collections.Generic
{
    internal class TextureCache : ITextureCache
    {
        private readonly ICache<string, Texture> _cache = new Cache<string, Texture>();
        private readonly IAssetService _assetService;
        private readonly Texture _invalidTexture = new Texture(1, 1);

        public TextureCache(IAssetService assetService) {
            this._assetService = assetService;
        }

        public Texture GetTexture(string textureId) {
            if (this._cache.TryGetValue(textureId, out var cachedTexture)) {
                return cachedTexture;
            }

            var asset = this._assetService.Textures.GetAsset(textureId);

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
