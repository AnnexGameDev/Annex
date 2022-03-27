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
