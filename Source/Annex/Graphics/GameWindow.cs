using Annex.Graphics.Contexts;
using Annex.Graphics.Contexts.Sfml;

namespace Annex.Graphics
{
    public class GameWindow : Singleton
    {
        public GraphicsContext Context { get; private set; }
        public const uint RESOLUTION_WIDTH = 1000;
        public const uint RESOLUTION_HEIGHT = 1000;

        public GameWindow() {
            this.Context = new SfmlContext();
        }
    }
}
