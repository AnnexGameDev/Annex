using Annex.Graphics.Contexts;
using Annex.Graphics.Contexts.Sfml;

namespace Annex.Graphics
{
    public sealed class GameWindow : Singleton
    {
        public GraphicsContext Context { get; private set; }
        public const uint RESOLUTION_WIDTH = 960;
        public const uint RESOLUTION_HEIGHT = 640;

        static GameWindow() {
            Create<GameWindow>();
        }

        public GameWindow() {
            this.Context = new SfmlContext();
        }
        public static GameWindow Singleton => Get<GameWindow>();
    }
}
