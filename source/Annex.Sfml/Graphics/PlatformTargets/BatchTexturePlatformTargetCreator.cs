using Annex.Core.Graphics.Contexts;
using Annex.Sfml.Collections.Generic;

namespace Annex.Sfml.Graphics.PlatformTargets
{
    internal class BatchTexturePlatformTargetCreator : PlatformTargetCreator<BatchTexturePlatformTarget>
    {
        private readonly ITextureCache _textureCache;

        public BatchTexturePlatformTargetCreator(ITextureCache textureCache) {
            this._textureCache = textureCache;
        }

        protected override PlatformTarget CreatePlatformTargetFor(DrawContext drawContext) {
            return new BatchTexturePlatformTarget((BatchTextureContext)drawContext, this._textureCache);
        }

        protected override bool Supports(DrawContext drawContext) {
            return drawContext is BatchTextureContext;
        }
    }
}
