using Annex.Graphics.Contexts;
using Annex.Graphics.Contexts.Sfml;

namespace Annex.Graphics
{
    public class GameWindow : Singleton
    {
        public GraphicsContext Context { get; private set; }

        public GameWindow() {
            this.Context = new SfmlContext();
        }
    }
}
