using Annex.Events;
using Annex.Graphics.Cameras;
using Annex.Graphics.Contexts;
using Annex.Graphics.Events;
using Annex.Resources;
using Annex.Scenes;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

namespace Annex.Graphics.Sfml
{
    internal class SfmlCanvas : ICanvas
    {
        private bool _usingUiView;
        private readonly View _uiView;
        private readonly View _gameContentView;
        private readonly Camera _camera;
        private readonly RenderWindow _buffer;
        private readonly ResourceManager<Texture> _textures;
        private readonly ResourceManager<Font> _fonts;

        private float _lastMouseClickX;
        private float _lastMouseClickY;
        private long _lastMouseClick;

        internal SfmlCanvas() {
            this._camera = new Camera();
            this._uiView = new View(new Vector2f(GameWindow.RESOLUTION_WIDTH / 2, GameWindow.RESOLUTION_HEIGHT / 2), new Vector2f(GameWindow.RESOLUTION_WIDTH, GameWindow.RESOLUTION_HEIGHT));
            this._gameContentView = new View();

            this._textures = new LazyResourceManager<Texture>("textures/", (path) => new Texture(path), (path) => path.EndsWith("png"));
            this._fonts = new LazyResourceManager<Font>("fonts/", (path) => new Font(path), (path) => path.EndsWith(".ttf"));
            this._buffer = new RenderWindow(new VideoMode(GameWindow.RESOLUTION_WIDTH, GameWindow.RESOLUTION_HEIGHT), "Window");

            var scenes = SceneManager.Singleton;
            this._buffer.Closed += (sender, e) => { scenes.CurrentScene.HandleCloseButtonPressed(); };
            this._buffer.KeyPressed += (sender, e) => {
                scenes.CurrentScene.HandleKeyboardKeyPressed(new KeyboardKeyPressedEvent() {
                    Key = e.Code.ToNonSFML(),
                    ShiftDown = e.Shift
                }); 
            };
            this._buffer.KeyReleased += (sender, e) => { 
                scenes.CurrentScene.HandleKeyboardKeyReleased(new KeyboardKeyReleasedEvent() {
                    Key = e.Code.ToNonSFML(),
                    ShiftDown = e.Shift
                });
            };
            this._buffer.MouseButtonPressed += (sender, e) => {
                var mousePos = Mouse.GetPosition(this._buffer);
                var gamePos = this._buffer.MapPixelToCoords(mousePos, this._gameContentView);
                var uiPos = this._buffer.MapPixelToCoords(mousePos, this._uiView);

                bool doubleClick = false;
                float dx = uiPos.X - this._lastMouseClickX;
                float dy = uiPos.Y - this._lastMouseClickY;
                long dt = EventManager.CurrentTime - this._lastMouseClick;
                int distanceThreshold = 10;
                int timeThreshold = 250;

                if (Math.Sqrt(dx * dx + dy * dy) < distanceThreshold && dt < timeThreshold) {
                    doubleClick = true;
                }

                this._lastMouseClickX = uiPos.X;
                this._lastMouseClickY = uiPos.Y;
                this._lastMouseClick = EventManager.CurrentTime;

                var scene = scenes.CurrentScene;
                scene.HandleSceneFocusMouseDown((int)uiPos.X, (int)uiPos.Y);
                scene.HandleMouseButtonPressed(new MouseButtonPressedEvent() {
                    Button = e.Button.ToNonSFML(),
                    MouseX = (int)uiPos.X,
                    MouseY = (int)uiPos.Y,
                    WorldX = gamePos.X,
                    WorldY = gamePos.Y,
                    DoubleClick = doubleClick
                });
            };
            this._buffer.MouseButtonReleased += (sender, e) => {
                var mousePos = Mouse.GetPosition(this._buffer);
                var gamePos = this._buffer.MapPixelToCoords(mousePos, this._gameContentView);
                var uiPos = this._buffer.MapPixelToCoords(mousePos, this._uiView);
                scenes.CurrentScene.HandleMouseButtonReleased(new MouseButtonReleasedEvent() {
                    Button = e.Button.ToNonSFML(),
                    MouseX = (int)uiPos.X,
                    MouseY = (int)uiPos.Y,
                    WorldX = gamePos.X,
                    WorldY = gamePos.Y,
                    TimeSinceClick = EventManager.CurrentTime - this._lastMouseClick
                });
            };
            this._buffer.JoystickButtonPressed += (sender, e) => {
                scenes.CurrentScene.HandleJoystickButtonPressed(new JoystickButtonPressedEvent() {
                    JoystickID = e.JoystickId,
                    Button = (JoystickButton)e.Button
                });
            };
            this._buffer.JoystickButtonReleased += (sender, e) => {
                scenes.CurrentScene.HandleJoystickButtonReleased(new JoystickButtonReleasedEvent() {
                    JoystickID = e.JoystickId,
                    Button = (JoystickButton)e.Button
                });
            };
            this._buffer.JoystickConnected += (sender, e) => {
                scenes.CurrentScene.HandleJoystickConnected(new JoystickConnectedEvent() {
                    JoystickID = e.JoystickId
                });
            };
            this._buffer.JoystickDisconnected += (sender, e) => {
                scenes.CurrentScene.HandleJoystickDisconnected(new JoystickDisconnectedEvent() {
                    JoystickID = e.JoystickId
                });
            };
            this._buffer.JoystickMoved += (sender, e) => {
                scenes.CurrentScene.HandleJoystickMoved(new JoystickMovedEvent() {
                    JoystickID = e.JoystickId,
                    Axis = e.Axis.ToNonSFML(),
                    Delta = e.Position
                });
            };
        }

        public void Draw(TextContext ctx) {

            if (String.IsNullOrEmpty(ctx.RenderText)) {
                return;
            }

            // We need to update the camera.
            this.UpdateView(ctx);

#pragma warning disable CS8604 // Possible null reference argument.
            // Prevented because of the null or empty check at the top.
            var font = this.GetFont(ctx.FontName);
#pragma warning restore CS8604 // Possible null reference argument.
            var text = new Text(ctx.RenderText, font) {
                CharacterSize = (uint)(int)ctx.FontSize,
                FillColor = ctx.FontColor,
                OutlineThickness = ctx.BorderThickness,
                OutlineColor = ctx.BorderColor,
                Position = ctx.RenderPosition
            };
            if (ctx.Alignment != null) {
                var offset = new Vector2f();

#pragma warning disable CS8602 // Dereference of a possibly null reference.
                // Prevented because of the null or empty check at the top.
                var bound = text.GetLocalBounds();
#pragma warning restore CS8602 // Dereference of a possibly null reference.

                switch (ctx.Alignment.HorizontalAlignment) {
                    case HorizontalAlignment.Center:
                        offset.X += (ctx.Alignment.Size.X / 2) - (bound.Width / 2);
                        break;
                    case HorizontalAlignment.Right:
                        offset.X += ctx.Alignment.Size.X - bound.Width;
                        break;
                }
                switch (ctx.Alignment.VerticalAlignment) {
                    case VerticalAlignment.Middle:
                        offset.Y += (ctx.Alignment.Size.Y / 2) - (ctx.FontSize.Value / 2);
                        break;
                    case VerticalAlignment.Bottom:
                        offset.Y += ctx.Alignment.Size.Y - ctx.FontSize.Value;
                        break;
                }
                text.Position += offset;
            }

            this._buffer.Draw(text);
        }

        public void Draw(SpriteSheetContext sheet) {

            if (String.IsNullOrEmpty(sheet.SourceTextureName)) {
                return;
            }

            if (sheet.SourceTextureRect == null) {
                using var sprite = this.GetSprite(sheet.SourceTextureName);
                var size = sprite.Texture.Size;

                sheet.SourceTextureRect = new Data.Shared.IntRect();
                int width = (int)(size.X / sheet.NumColumns);
                int height = (int)(size.Y / sheet.NumRows);
                sheet.SourceTextureRect.Width.Set(width);
                sheet.SourceTextureRect.Height.Set(height);
            }

            sheet.SourceTextureRect.Top.Set(sheet.SourceTextureRect.Height * sheet.Row);
            sheet.SourceTextureRect.Left.Set(sheet.SourceTextureRect.Width * sheet.Column);

            this.Draw(sheet._internalTexture);
        }

        public void Draw(TextureContext ctx) {

            if (String.IsNullOrEmpty(ctx.SourceTextureName)) {
                return;
            }

            // We need to update the camera.
            this.UpdateView(ctx);

#pragma warning disable CS8604 // Possible null reference argument.
            // Prevented because of the null or empty check at the top.x`
            var sprite = this.GetSprite(ctx.SourceTextureName);
#pragma warning restore CS8604 // Possible null reference argument.

            sprite.Position = ctx.RenderPosition;

            Vector2f renderSize;
            if (ctx.RenderSize == null) {
                if (ctx.SourceTextureRect == null) {
                    renderSize = new Vector2f(sprite.Texture.Size.X, sprite.Texture.Size.Y);
                } else {
                    renderSize = new Vector2f(ctx.SourceTextureRect.Width, ctx.SourceTextureRect.Height);
                }
            } else {
                renderSize = ctx.RenderSize;
            }

            if (ctx.SourceTextureRect != null) {
                sprite.TextureRect = ctx.SourceTextureRect;
                sprite.Scale = new Vector2f(renderSize.X / ctx.SourceTextureRect.Width, renderSize.Y / ctx.SourceTextureRect.Height);
            } else {
                sprite.Scale = new Vector2f(renderSize.X / sprite.Texture.Size.X, renderSize.Y / sprite.Texture.Size.Y);
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

        public void Draw(SolidRectangleContext rectangle) {
            this.UpdateView(rectangle);

            var shape = new RectangleShape {
                Size = rectangle.RenderSize,
                Position = rectangle.RenderPosition,
                FillColor = rectangle.RenderFillColor
            };

            if (rectangle.RenderBorderColor != null && rectangle.RenderBorderSize != 0) {
                shape.OutlineColor = rectangle.RenderBorderColor;
                shape.OutlineThickness = rectangle.RenderBorderSize;
            }

            this._buffer.Draw(shape);
        }

        private Sprite GetSprite(string textureName) {
            return new Sprite(this._textures.GetResource(textureName));
        }

        private Font GetFont(string fontName) {
            return this._fonts.GetResource(fontName);
        }

        public void BeginDrawing() {
            this._buffer.Clear();
            this._buffer.DispatchEvents();
            Joystick.Update();
            this.UpdateGameContentCamera();
        }

        public void EndDrawing() {
            this._buffer.Display();
        }

        private void UpdateGameContentCamera() {
            this._gameContentView.Reset(new FloatRect(0, 0, 1, 1));
            this._gameContentView.Size = this._camera.Size;
            this._gameContentView.Zoom(this._camera.CurrentZoom);
            this._gameContentView.Center = new Vector2f((int)this._camera.Centerpoint.X, (int)this._camera.Centerpoint.Y);
            this._buffer.SetView(this._gameContentView);
            this._usingUiView = false;
        }

        public void SetVisible(bool visible) {
            this._buffer.SetVisible(visible);
        }

        public bool IsMouseButtonDown(MouseButton button) {
            var sfmlButton = button.ToSFML();
            return Mouse.IsButtonPressed(sfmlButton);
        }

        public bool IsKeyDown(KeyboardKey key) {
            var sfmlKey = key.ToSFML();
            return sfmlKey != Keyboard.Key.Unknown ? Keyboard.IsKeyPressed(sfmlKey) : false;
        }

        public Camera GetCamera() {
            return this._camera;
        }

        public void Destroy() {
            this._buffer.Close();
        }

        public Data.Shared.Vector GetRealMousePos() {
            var realpos = Mouse.GetPosition(this._buffer);
            var pos = this._buffer.MapPixelToCoords(realpos, this._uiView);
            return Data.Shared.Vector.Create(pos.X, pos.Y);
        }

        private void UpdateView(DrawingContext ctx) {
            if (ctx.UseUIView != this._usingUiView) {
                if (ctx.UseUIView) {
                    this._buffer.SetView(this._uiView);
                    this._usingUiView = true;
                } else {
                    this._buffer.SetView(this._gameContentView);
                    this._usingUiView = false;
                }
            }
        }

        public bool IsJoystickConnected(uint joystickId) {
            return Joystick.IsConnected(joystickId);
        }

        public bool IsJoystickButtonPressed(uint joystickId, JoystickButton button) {
            return Joystick.IsButtonPressed(joystickId, (uint)button);
        }

        public float GetJoystickAxis(uint joystickId, JoystickAxis axis) {
            return Joystick.GetAxisPosition(joystickId, (Joystick.Axis)axis);
        }
    }
}
