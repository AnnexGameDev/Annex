using Annex.Core.Graphics;
using Annex.Core.Graphics.Contexts;
using Annex.Sfml.Collections.Generic;
using Annex.Sfml.Graphics.Transforms;
using SFML.Graphics;

namespace Annex.Sfml.Graphics.Windows
{
    internal abstract class SfmlCanvas : ICanvas
    {
        private readonly ITextureCache _textureCache;
        protected abstract RenderTarget? _renderTarget { get; }

        public SfmlCanvas(ITextureCache textureCache) {
            this._textureCache = textureCache;
        }

        public void Draw(SpritesheetContext context) {
        }

        public void Draw(TextureContext context) {
            if (context.PlatformTarget is not SpritePlatformTarget sprite) {
                sprite = new SpritePlatformTarget(context, this._textureCache);
            }
            sprite.Draw(this._renderTarget);
        }
    }
}