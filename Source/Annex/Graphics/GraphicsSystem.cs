using Annex.Graphics.Contexts;
using Annex.Graphics.Contexts.Sfml;

namespace Annex.Graphics
{
    public class GraphicsSystem : Singleton, IDrawableContext
    {
        private GraphicsContext _context;

        public GraphicsSystem() {
            this._context = new SfmlContext();
        }

        public void Draw(TextContext ctx) {
            _context.Draw(ctx);
        }

        public void Draw(SurfaceContext ctx) {
            _context.Draw(ctx);
        }
    }
}
