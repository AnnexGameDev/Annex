using Annex.Core.Assets;
using Annex.Core.Collections.Generic;
using SFML.Graphics;

namespace Annex.Sfml.Collections.Generic
{
    internal class FontCache : IFontCache
    {
        private readonly ICache<string, Font> _cache = new Cache<string, Font>();
        private readonly IAssetService _assetService;

        public FontCache(IAssetService assetService) {
            this._assetService = assetService;
        }

        public Font GetFont(string fontId) {

            if (this._cache.TryGetValue(fontId, out var font)) {
                return font;
            }

            var asset = this._assetService.Fonts.GetAsset(fontId);

            if (asset is not Font newFont) {
                if (asset.FilepathSupported) {
                    newFont = new Font(asset.FilePath);
                } else {
                    newFont = new Font(asset.ToBytes());
                }
            }

            this._cache.Add(fontId, newFont);
            return newFont;
        }
    }
}
