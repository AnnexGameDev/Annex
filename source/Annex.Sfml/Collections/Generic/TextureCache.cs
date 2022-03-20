using Annex.Core.Assets;
using Annex.Core.Collections.Generic;
using SFML.Graphics;

namespace Annex.Sfml.Collections.Generic
{
    internal class TextureCache : ITextureCache
    {
        private readonly ICache<string, Texture> _cache = new Cache<string, Texture>();
        private readonly IAssetService _assetService;

        public TextureCache(IAssetService assetService) {
            this._assetService = assetService;
        }

        public Texture GetTexture(string textureId) {
            if (this._cache.TryGetValue(textureId, out var cachedTexture)) {
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

            this._cache.Add(textureId, newTexture);
            return newTexture;
        }
    }
}
