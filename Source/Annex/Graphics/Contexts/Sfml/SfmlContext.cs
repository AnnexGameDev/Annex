using Annex.Resources;
using SFML.Graphics;
using SFML.Window;

namespace Annex.Graphics.Contexts.Sfml
{
    public class SfmlContext : GraphicsContext
    {
        private RenderWindow _buffer;
        private ResourceManager<Texture> _textures;
        private ResourceManager<Font> _fonts;

        public SfmlContext() {
            this._textures = new ResourceManager<Texture>("/textures/", (path) => new Texture(path), true, (path) => path.EndsWith("png"));
            this._fonts = new ResourceManager<Font>("/fonts/", (path) => new Font(path), true, (path) => path.EndsWith(".ttf"));
            this._buffer = new RenderWindow(new VideoMode(1000, 1000), "Window");
        }

        public override void Draw(TextContext ctx) {
        }

        public override void Draw(SurfaceContext ctx) {
        }

        public override void BeginDrawing() {
            this._buffer.Clear();
            this._buffer.DispatchEvents();
        }

        public override void EndDrawing() {
            this._buffer.Display();
        }

        public override void SetVisible(bool visible) {
            this._buffer.SetVisible(visible);
        }
    }
}
