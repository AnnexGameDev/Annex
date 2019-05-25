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

        public override bool IsMouseButtonDown(MouseButton button) {
            Mouse.Button? sfmlButton = null;
            switch (sfmlButton) {
                case Mouse.Button.Left:
                    sfmlButton = Mouse.Button.Left;
                    break;
                case Mouse.Button.Right:
                    sfmlButton = Mouse.Button.Right;
                    break;
            }
            if (sfmlButton == null) {
                return false;
            }
            return Mouse.IsButtonPressed((Mouse.Button)sfmlButton);
        }

        public override bool IsKeyDown(Key key) {
            Keyboard.Key? sfmlKey = null;
            switch (key) {
                case Key.ArrowKey_Down:
                    sfmlKey = Keyboard.Key.Down;
                    break;
                case Key.ArrowKey_Left:
                    sfmlKey = Keyboard.Key.Left;
                    break;
                case Key.ArrowKey_Up:
                    sfmlKey = Keyboard.Key.Up;
                    break;
                case Key.ArrowKey_Right:
                    sfmlKey = Keyboard.Key.Right;
                    break;
            }
            if (sfmlKey == null) {
                return false;
            }
            return Keyboard.IsKeyPressed((Keyboard.Key)sfmlKey);
        }
    }
}
