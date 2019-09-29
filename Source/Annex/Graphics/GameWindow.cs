using Annex.Graphics.Contexts;
using Annex.Graphics.Contexts.Sfml;

namespace Annex.Graphics
{
    public sealed class GameWindow : Singleton
    {
        public GraphicsContext Context { get; private set; }
        public static uint RESOLUTION_WIDTH { get; private set; } = 960;
        public static uint RESOLUTION_HEIGHT { get; private set; } = 640;

        public static GameWindow Singleton => Get<GameWindow>();
        static GameWindow() {
            Create<GameWindow>();
        }

        public GameWindow() {
            this.Context = new SfmlContext();
        }

        public void ChangeResolution(uint width, uint height) {
            GameWindow.RESOLUTION_WIDTH = width;
            GameWindow.RESOLUTION_HEIGHT = height;

            var oldCamera = this.Context.GetCamera();
            this.Context.Destroy();

            this.Context = new SfmlContext();
            this.Context.GetCamera().Copy(oldCamera);
        }
    }
}
