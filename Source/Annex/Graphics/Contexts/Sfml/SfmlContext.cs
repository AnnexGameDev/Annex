using Annex.Graphics.Cameras;
using Annex.Resources;
using Annex.Scenes;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

namespace Annex.Graphics.Contexts.Sfml
{
    public class SfmlContext : GraphicsContext
    {
        private bool _usingUiView;
        private readonly View _uiView;
        private readonly View _gameContentView;
        private readonly Camera _camera;
        private readonly RenderWindow _buffer;
        private readonly ResourceManager<Texture> _textures;
        private readonly ResourceManager<Font> _fonts;

        public SfmlContext() {
            this._camera = new Camera();
            this._uiView = new View(new Vector2f(GameWindow.RESOLUTION_WIDTH / 2, GameWindow.RESOLUTION_HEIGHT / 2), new Vector2f(GameWindow.RESOLUTION_WIDTH, GameWindow.RESOLUTION_HEIGHT));
            this._gameContentView = new View();

            this._textures = new LazyResourceManager<Texture>("textures/", (path) => new Texture(path), (path) => path.EndsWith("png"));
            this._fonts = new LazyResourceManager<Font>("fonts/", (path) => new Font(path), (path) => path.EndsWith(".ttf"));
            this._buffer = new RenderWindow(new VideoMode(GameWindow.RESOLUTION_WIDTH, GameWindow.RESOLUTION_HEIGHT), "Window");

            var scenes = SceneManager.Singleton;
            this._buffer.Closed += (sender, e) => { scenes.CurrentScene.HandleCloseButtonPressed(); };
            this._buffer.KeyPressed += (sender, e) => { scenes.CurrentScene.HandleKeyboardKeyPressed(e.Code.ToNonSFML()); };
            this._buffer.KeyReleased += (sender, e) => { scenes.CurrentScene.HandleKeyboardKeyReleased(e.Code.ToNonSFML()); };
            this._buffer.MouseButtonPressed += (sender, e) => {
                var mousePos = Mouse.GetPosition(this._buffer);
                var gamePos = _buffer.MapPixelToCoords(mousePos, this._gameContentView);
                var scene = scenes.CurrentScene;
                scene.HandleSceneFocusMouseDown(mousePos.X, mousePos.Y);
                scene.HandleMouseButtonPressed(e.Button.ToNonSFML(), gamePos.X, gamePos.Y, mousePos.X, mousePos.Y);
            };
            this._buffer.MouseButtonReleased += (sender, e) => {
                var mousePos = Mouse.GetPosition(this._buffer);
                var gamePos = _buffer.MapPixelToCoords(mousePos, this._gameContentView);
                scenes.CurrentScene.HandleMouseButtonReleased(e.Button.ToNonSFML(), gamePos.X, gamePos.Y, mousePos.X, mousePos.Y);
            };

            // TODO: Attach event handlers.
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

            if (String.IsNullOrEmpty(ctx.RenderText)) {
                return;
            }

            // We need to update the camera.
            if (ctx.UseUIView != this._usingUiView) {
                if (ctx.UseUIView) {
                    this._buffer.SetView(this._uiView);
                    this._usingUiView = true;
                } else {
                    this._buffer.SetView(this._gameContentView);
                    this._usingUiView = false;
                }
            }

#pragma warning disable CS8604 // Possible null reference argument.
            // Prevented because of the null or empty check at the top.
            var font = this.GetFont(ctx.FontName);
#pragma warning restore CS8604 // Possible null reference argument.
            var text = new Text(ctx.RenderText, font) {
                CharacterSize = ctx.FontSize,
                FillColor = ctx.FontColor,
                OutlineThickness = ctx.BorderThickness,
                OutlineColor = ctx.BorderColor,
                Position = ctx.RenderPosition
            };
            if (ctx.Alignment != null) {
                var offset = new Vector2f();

#pragma warning disable CS8602 // Dereference of a possibly null reference.
                // Prevented because of the null or empty check at the top.
                var end = text.FindCharacterPos((uint)(ctx.RenderText.Value.Length - 1));
#pragma warning restore CS8602 // Dereference of a possibly null reference.

                switch (ctx.Alignment.HorizontalAlignment) {
                    case HorizontalAlignment.Center:
                        offset.X += (ctx.Alignment.Size.X / 2) - (end.X / 2);
                        break;
                    case HorizontalAlignment.Right:
                        offset.X += ctx.Alignment.Size.X - end.X;
                        break;
                }
                switch (ctx.Alignment.VerticalAlignment) {
                    case VerticalAlignment.Middle:
                        offset.Y += (ctx.Alignment.Size.Y / 2) - (end.Y / 2);
                        break;
                    case VerticalAlignment.Bottom:
                        offset.Y += ctx.Alignment.Size.Y - end.Y;
                        break;
                }
                text.Position += offset;
            }

            this._buffer.Draw(text);
        }

        public override void Draw(SurfaceContext ctx) {

            if (String.IsNullOrEmpty(ctx.SourceSurfaceName)) {
                return;
            }

            // We need to update the camera.
            if (ctx.UseUIView != this._usingUiView) {
                if (ctx.UseUIView) {
                    this._buffer.SetView(this._uiView);
                    this._usingUiView = true;
                } else {
                    this._buffer.SetView(this._gameContentView);
                    this._usingUiView = false;
                }
            }

#pragma warning disable CS8604 // Possible null reference argument.
            // Prevented because of the null or empty check at the top.x`
            var sprite = this.GetSprite(ctx.SourceSurfaceName);
#pragma warning restore CS8604 // Possible null reference argument.

            sprite.Position = ctx.RenderPosition;

            if (ctx.SourceSurfaceRect != null) {
                sprite.TextureRect = ctx.SourceSurfaceRect;
                sprite.Scale = new Vector2f(ctx.RenderSize.X / ctx.SourceSurfaceRect.Width, ctx.RenderSize.Y / ctx.SourceSurfaceRect.Height);
            } else {
                sprite.Scale = new Vector2f(ctx.RenderSize.X / sprite.Texture.Size.X, ctx.RenderSize.Y / sprite.Texture.Size.Y);
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
            this.UpdateGameContentCamera();
        }

        public override void EndDrawing() {
            this._buffer.Display();
        }

        private void UpdateGameContentCamera() {
            this._gameContentView.Reset(new FloatRect(0, 0, 1, 1));
            this._gameContentView.Size = this._camera.Size;
            this._gameContentView.Zoom(this._camera.CurrentZoom);
            this._gameContentView.Center = this._camera.Centerpoint;
            this._buffer.SetView(this._gameContentView);
            this._usingUiView = false;
        }

        public override void SetVisible(bool visible) {
            this._buffer.SetVisible(visible);
        }

        public override bool IsMouseButtonDown(MouseButton button) {
            var sfmlButton = button.ToSFML();
            return Mouse.IsButtonPressed(sfmlButton);
        }

        public override bool IsKeyDown(KeyboardKey key) {
            var sfmlKey = key.ToSFML();
            return sfmlKey != Keyboard.Key.Unknown ? Keyboard.IsKeyPressed(sfmlKey) : false;
        }

        public override Camera GetCamera() {
            return this._camera;
        }
    }
}
