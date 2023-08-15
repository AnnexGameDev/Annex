using Annex_Old.Graphics.Contexts;
using SFML.Graphics;
using SFML.System;

namespace Annex_Old.Graphics.Sfml.Targets
{
    internal class TexturePlatformTarget : ITexturePlatformTarget
    {
        public string? TextureName;
        public Sprite Sprite;
        public Vector2f RenderSize;
        public Vector2f RenderPosition;
        public Vector2f Scale;
        public Vector2f Origin;

        public TexturePlatformTarget() {
            this.Sprite = new Sprite();
            this.RenderSize = new Vector2f();
        }

        public void Dispose() {
            this.Sprite.Dispose();
        }
    }
}
