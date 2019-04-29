using Annex.Resources;
using SFML.Graphics;

namespace Annex.Graphics.Contexts.Sfml
{
    public class SfmlContext : GraphicsContext
    {
        private ResourceManager<Texture> _textures;
        private ResourceManager<Font> _fonts;

        public SfmlContext() {
            this._textures = new ResourceManager<Texture>("/textures/", (path) => new Texture(path), true, (path) => path.EndsWith("png"));
            this._fonts = new ResourceManager<Font>("/fonts/", (path) => new Font(path), true, (path) => path.EndsWith(".ttf"));
        }

        public override void Draw(TextContext ctx) {
        }

        public override void Draw(SurfaceContext ctx) {
        }
    }
}
