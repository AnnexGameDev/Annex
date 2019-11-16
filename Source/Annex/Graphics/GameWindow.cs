using Annex.Graphics.Sfml;

namespace Annex.Graphics
{
    public sealed class GameWindow : Singleton
    {
        public const string DrawGameEventID = "draw-game";
        public ICanvas Canvas { get; private set; }
        public static uint RESOLUTION_WIDTH { get; private set; } = 960;
        public static uint RESOLUTION_HEIGHT { get; private set; } = 640;

        public static GameWindow Singleton => Get<GameWindow>();
        static GameWindow() {
            Create<GameWindow>();
        }

        public GameWindow() {
            this.Canvas = new SfmlCanvas();
        }

        public void ChangeResolution(uint width, uint height) {
            RESOLUTION_WIDTH = width;
            RESOLUTION_HEIGHT = height;

            var oldCamera = this.Canvas.GetCamera();
            this.Canvas.Destroy();

            this.Canvas = new SfmlCanvas();
            this.Canvas.GetCamera().Copy(oldCamera);
        }
    }
}
