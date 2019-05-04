using Annex.Resources;
using Annex.UserInterface;
using SFML.Graphics;
using SFML.Window;

namespace Annex.Graphics.Contexts.Sfml
{
    public class SfmlContext : GraphicsContext
    {
        private readonly RenderWindow _buffer;
        private readonly ResourceManager<Texture> _textures;
        private readonly ResourceManager<Font> _fonts;

        public SfmlContext() {
            this._textures = new ResourceManager<Texture>("textures/", (path) => new Texture(path), true, (path) => path.EndsWith("png"));
            this._fonts = new ResourceManager<Font>("fonts/", (path) => new Font(path), true, (path) => path.EndsWith(".ttf"));
            this._buffer = new RenderWindow(new VideoMode(1000, 1000), "Window");

            // TODO: Attach event handlers.
            var ui = Singleton.Get<UI>();

            //this._buffer.Closed += ();
            //this._buffer.KeyPressed += ();
            //this._buffer.KeyReleased += ();
            //this._buffer.MouseButtonPressed += ();
            //this._buffer.MouseButtonReleased += ();
            //this._buffer.JoystickButtonPressed += ();
            //this._buffer.JoystickButtonReleased += ();
            //this._buffer.JoystickConnected += ();
            //this._buffer.JoystickDisconnected += ();
            //this._buffer.JoystickMoved += ();

            // TODO: Expose input state API.
            //Keyboard.IsKeyPressed(Keyboard.Key.);
            //Mouse.IsButtonPressed(Mouse.Button.);
            //Joystick.IsButtonPressed(, );
            //Joystick.IsConnected(, );
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
