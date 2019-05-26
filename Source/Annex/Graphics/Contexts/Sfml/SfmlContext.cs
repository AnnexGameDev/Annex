using Annex.Resources;
using Annex.UserInterface;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Annex.Graphics.Contexts.Sfml
{
    public class SfmlContext : GraphicsContext
    {
        private readonly RenderWindow _buffer;
        private readonly ResourceManager<Texture> _textures;
        private readonly ResourceManager<Font> _fonts;

        public SfmlContext() {
            this._textures = new LazyResourceManager<Texture>("textures/", (path) => new Texture(path), (path) => path.EndsWith("png"));
            this._fonts = new LazyResourceManager<Font>("fonts/", (path) => new Font(path), (path) => path.EndsWith(".ttf"));
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
            //Joystick.IsButtonPressed(, );
            //Joystick.IsConnected(, );
        }

        public override void Draw(TextContext ctx) {

            if (string.IsNullOrEmpty(ctx.RenderText)) {
                return;
            }

            var font = GetFont(ctx.FontName);
            var text = new Text(ctx.RenderText, font);

            text.CharacterSize = ctx.FontSize;
            text.FillColor = ctx.FontColor;
            text.OutlineThickness = ctx.BorderThickness;
            text.OutlineColor = ctx.BorderColor;

            text.Position = ctx.RenderPosition;
            if (ctx.Aliignment != null) {
                var offset = new Vector2f();

                var end = text.FindCharacterPos((uint)(ctx.RenderText.Length - 1));

                switch (ctx.Aliignment.HorizontalAlignment) {
                    case HorizontalAlignment.Center:
                        offset.X += (ctx.Aliignment.Size.X / 2) - (end.X / 2);
                        break;
                    case HorizontalAlignment.Right:
                        offset.X += ctx.Aliignment.Size.X - end.X;
                        break;
                }
                switch (ctx.Aliignment.VerticalAlignment) {
                    case VerticalAlignment.Middle:
                        offset.Y += (ctx.Aliignment.Size.Y / 2) - (end.Y / 2);
                        break;
                    case VerticalAlignment.Bottom:
                        offset.Y += ctx.Aliignment.Size.Y - end.Y;
                        break;
                }
                text.Position += offset;
            }

            this._buffer.Draw(text);
        }

        public override void Draw(SurfaceContext ctx) {
            var sprite = GetSprite(ctx.SourceSurfaceName);

            sprite.Position = ctx.RenderPosition;

            if (ctx.SourceSurfaceRect != null) {
                sprite.TextureRect = ctx.SourceSurfaceRect;
                sprite.Scale = new Vector2f(ctx.RenderSize.X / ctx.SourceSurfaceRect.Width, ctx.RenderSize.Y / ctx.SourceSurfaceRect.Height);
            } else {
                sprite.Scale = new Vector2f(ctx.RenderSize.X / sprite.Texture.Size.X, ctx.RenderSize.Y / sprite.Texture.Size.Y);
            }

            // TODO: relative positioning based off of the camera
            if (!ctx.IsAbsolute) {

            }

            if (ctx.RenderColor != null) {
                sprite.Color = ctx.RenderColor;
            }

            if (ctx.Rotation % 360 != 0) {
                sprite.Origin = ctx.RelativeRotationOrigin;
                sprite.Rotation = ctx.Rotation;
                sprite.Position += new Vector2f(ctx.RelativeRotationOrigin.X, ctx.RelativeRotationOrigin.Y);
            }

            this._buffer.Draw(sprite);
        }

        private Sprite GetSprite(string surfaceName) {
            return new Sprite(this._textures.GetResource(surfaceName));
        }

        private Font GetFont(string fontName) {
            return this._fonts.GetResource(fontName);
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
