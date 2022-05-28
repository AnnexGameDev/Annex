using Annex_Old.Core.Graphics.Contexts;
using Annex_Old.Sfml.Collections.Generic;

namespace Annex_Old.Sfml.Graphics.PlatformTargets
{
    internal class TexturePlatformTargetCreator : PlatformTargetCreator<TexturePlatformTarget>
    {
        private readonly ITextureCache _textureCache;

        public TexturePlatformTargetCreator(ITextureCache textureCache) {
            this._textureCache = textureCache;
        }

        protected override PlatformTarget CreatePlatformTargetFor(DrawContext drawContext) {
            return new TexturePlatformTarget((TextureContext)drawContext, this._textureCache);
        }

        protected override bool Supports(DrawContext drawContext) {
            return drawContext is TextureContext;
        }
    }
}
