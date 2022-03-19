using Annex.Core.Collections.Generic;
using Annex.Core.Graphics.Contexts;
using SFML.Graphics;

namespace Annex.Sfml.Graphics.Transforms
{
    public class SpriteContext : IDisposable
    {
        public readonly Transform Transform = Transform.Identity;
        public readonly Transformable Sprite;
        public readonly RenderStates State = new RenderStates();

        private readonly TextureContext _context;
        private readonly ICache<string, Texture> _textureCache;

        public SpriteContext(TextureContext context, ICache<string, Texture> textureCache) {
            this._context = context;
            this._textureCache = textureCache;
        }

        public void Dispose() {
            this.Sprite.Dispose();
        }

        public void Draw(RenderTarget? renderTarget) {
            if (renderTarget == null)
                return;

            UpdateTransform();
        }

        private void UpdateTransform() {
        }
    }
}
