using Annex_Old.Core.Graphics.Contexts;
using Annex_Old.Sfml.Collections.Generic;

namespace Annex_Old.Sfml.Graphics.PlatformTargets
{
    internal class TextPlatformTargetCreator : PlatformTargetCreator<TextPlatformTarget>
    {
        private readonly IFontCache _fontCache;

        public TextPlatformTargetCreator(IFontCache fontCache) {
            this._fontCache = fontCache;
        }

        protected override PlatformTarget CreatePlatformTargetFor(DrawContext drawContext) {
            return new TextPlatformTarget((TextContext)drawContext, this._fontCache);
        }

        protected override bool Supports(DrawContext drawContext) {
            return drawContext is TextContext;
        }
    }
}
