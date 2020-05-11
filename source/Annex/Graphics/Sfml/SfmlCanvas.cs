#nullable enable
using Annex.Data.Shared;
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
using System.IO;
using System.Threading;

namespace Annex.Graphics.Sfml
{
    public class SfmlCanvas : Canvas
    {
        private Mutex _bufferAccess;
        private bool _usingUiView;
        private readonly View _uiView;
        private readonly View _gameContentView;
        private readonly Camera _camera;
        private RenderWindow _buffer;

        private float _lastMouseClickX;
        private float _lastMouseClickY;
        private long _lastMouseClick;

        private readonly Data.Shared.Vector _resolution;
        private string _title { get; set; }
        private Styles _style { get; set; }

        public readonly ResourceManager FontManager;
        public readonly ResourceManager TextureManager;

        public SfmlCanvas() : this(960, 640) {

        }

        internal SfmlCanvas(float resolutionWidth, float resolutionHeight) {
            this._bufferAccess = new Mutex();
            this._resolution = Data.Shared.Vector.Create(resolutionWidth, resolutionHeight);
            this._camera = new Camera(this._resolution);
            this._uiView = new View(new Vector2f(this._resolution.X / 2, this._resolution.Y / 2), new Vector2f(this._resolution.X, this._resolution.Y));
            this._gameContentView = new View();
            this._buffer = new RenderWindow(new SFML.Window.VideoMode((uint)this._resolution.X, (uint)this._resolution.Y), "", Styles.Default);
            this._style = Styles.Default;
            this.SetTitle("Window");

            SetupResourceManagers();
            AttachUIHandlers();
        }

        private void SetTitle(string title) {
            this._buffer.SetTitle(title);
            this._title = title;
        }

        private void ModifyBuffer(Data.Shared.Vector size, Styles style) {
            this.ModifyBuffer((uint)size.X, (uint)size.Y, style);
        }

        private void ModifyBuffer(uint x, uint y, Styles style) {
            this._bufferAccess.WaitOne();

            var ratio = Vector.Create();
            ratio.X = x / this._resolution.X;
            ratio.Y = y / this._resolution.Y;

            this._camera.Size.X *= ratio.X;
            this._camera.Size.Y *= ratio.Y;

            this._buffer.Close();
            this._buffer = new RenderWindow(new SFML.Window.VideoMode(x, y), this._title, style);
            this._style = style;
            this._resolution.Set(x, y);
            this.AttachUIHandlers();
            this._bufferAccess.ReleaseMutex();
        }

        private void SetupResourceManagers() {
            // Resources
            var resources = ServiceProvider.ResourceManagerRegistry;

            // TODO: Textures
            //if (!resources.Exists(ResourceType.Textures)) {
            //    var textures = resources.GetOrCreate<FSResourceManager>(ResourceType.Textures);
            //    textures.SetResourcePath(this.TexturePath);
            //    textures.SetResourceValidator(this.TextureValidator);
            //    textures.SetResourceLoader(this.TextureLoader_FromString);
            //    textures.SetResourceLoader(this.TextureLoader_FromBytes);
            //} else {
            //    ServiceProvider.Log.WriteLineWarning($"Resource manager for {ResourceType.Textures} already was set.");
            //}

            //// Fonts
            //if (!resources.Exists(ResourceType.Font)) {
            //    var fonts = resources.GetOrCreate<FSResourceManager>(ResourceType.Font);
            //    fonts.SetResourcePath(this.FontPath);
            //    fonts.SetResourceValidator(this.FontValidator);
            //    fonts.SetResourceLoader(this.FontLoader_FromString);
            //    fonts.SetResourceLoader(this.FontLoader_FromBytes);
            //} else {
            //    ServiceProvider.Log.WriteLineWarning($"Resource manager for {ResourceType.Font} already was set.");
            //}
        }

        private void AttachUIHandlers() {
            // Hook up UI events
            this._buffer.Closed += (sender, e) => { ServiceProvider.SceneManager.CurrentScene.HandleCloseButtonPressed(); };
            this._buffer.KeyPressed += (sender, e) => {
                ServiceProvider.SceneManager.CurrentScene.HandleKeyboardKeyPressed(new KeyboardKeyPressedEvent() {
                    Key = e.Code.ToNonSFML(),
                    ShiftDown = e.Shift
                });
            };
            this._buffer.KeyReleased += (sender, e) => {
                ServiceProvider.SceneManager.CurrentScene.HandleKeyboardKeyReleased(new KeyboardKeyReleasedEvent() {
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

                var scene = ServiceProvider.SceneManager.CurrentScene;
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
                ServiceProvider.SceneManager.CurrentScene.HandleMouseButtonReleased(new MouseButtonReleasedEvent() {
                    Button = e.Button.ToNonSFML(),
                    MouseX = (int)uiPos.X,
                    MouseY = (int)uiPos.Y,
                    WorldX = gamePos.X,
                    WorldY = gamePos.Y,
                    TimeSinceClick = EventManager.CurrentTime - this._lastMouseClick
                });
            };
            this._buffer.JoystickButtonPressed += (sender, e) => {
                ServiceProvider.SceneManager.CurrentScene.HandleJoystickButtonPressed(new JoystickButtonPressedEvent() {
                    JoystickID = e.JoystickId,
                    Button = (JoystickButton)e.Button
                });
            };
            this._buffer.JoystickButtonReleased += (sender, e) => {
                ServiceProvider.SceneManager.CurrentScene.HandleJoystickButtonReleased(new JoystickButtonReleasedEvent() {
                    JoystickID = e.JoystickId,
                    Button = (JoystickButton)e.Button
                });
            };
            this._buffer.JoystickConnected += (sender, e) => {
                ServiceProvider.SceneManager.CurrentScene.HandleJoystickConnected(new JoystickConnectedEvent() {
                    JoystickID = e.JoystickId
                });
            };
            this._buffer.JoystickDisconnected += (sender, e) => {
                ServiceProvider.SceneManager.CurrentScene.HandleJoystickDisconnected(new JoystickDisconnectedEvent() {
                    JoystickID = e.JoystickId
                });
            };
            this._buffer.JoystickMoved += (sender, e) => {
                ServiceProvider.SceneManager.CurrentScene.HandleJoystickMoved(new JoystickMovedEvent() {
                    JoystickID = e.JoystickId,
                    Axis = e.Axis.ToNonSFML(),
                    Delta = e.Position
                });
            };
        }

        public override void Draw(TextContext ctx) {

            if (System.String.IsNullOrEmpty(ctx.RenderText)) {
                return;
            }

            // We need to update the camera.
            this.UpdateView(ctx);

#pragma warning disable CS8604 // Possible null reference argument.
            // Prevented because of the null or empty check at the top.
            var font = new Font("");
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

        public override void Draw(SpriteSheetContext sheet) {

            if (System.String.IsNullOrEmpty(sheet.SourceTextureName)) {
                return;
            }

            if (sheet.SourceTextureRect == null) {
                //using var sprite = this.GetSprite(sheet.SourceTextureName);
                using var sprite = new Sprite();
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

        public override void Draw(TextureContext ctx) {

            if (System.String.IsNullOrEmpty(ctx.SourceTextureName)) {
                return;
            }

            // We need to update the camera.
            this.UpdateView(ctx);

#pragma warning disable CS8604 // Possible null reference argument.
            // Prevented because of the null or empty check at the top.x`
            //var sprite = this.GetSprite(ctx.SourceTextureName);
            using var sprite = new Sprite();
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

        public override void Draw(SolidRectangleContext rectangle) {
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

        internal override void BeginDrawing() {
            this._bufferAccess.WaitOne();
            this._buffer.Clear();
            this.UpdateGameContentCamera();
        }

        internal override void EndDrawing() {
            this._buffer.Display();
            this._bufferAccess.ReleaseMutex();
        }

        public override void SetVideoMode(VideoMode mode) {
            var style = Styles.Default;

            switch (mode) {
                case VideoMode.FullScreen:
                    style = Styles.Fullscreen;
                    break;
                case VideoMode.Window:
                    style = Styles.Default;
                    break;
                default:
                    Debug.Error($"Unknown VideoMode:{mode}");
                    break;
            }

            this.ModifyBuffer(this._resolution, style);
        }

        private void UpdateGameContentCamera() {
            this._gameContentView.Reset(new FloatRect(0, 0, 1, 1));
            this._gameContentView.Size = this._camera.Size;
            this._gameContentView.Zoom(this._camera.CurrentZoom);
            this._gameContentView.Center = new Vector2f((int)this._camera.Centerpoint.X, (int)this._camera.Centerpoint.Y);
            this._buffer.SetView(this._gameContentView);
            this._usingUiView = false;
        }

        public override void SetVisible(bool visible) {
            this._buffer.SetVisible(visible);
        }

        public override bool IsMouseButtonDown(MouseButton button) {
            this._bufferAccess.WaitOne();
            var sfmlButton = button.ToSFML();
            var isMouseButtonDown = Mouse.IsButtonPressed(sfmlButton);
            this._bufferAccess.ReleaseMutex();
            return isMouseButtonDown;
        }

        public override bool IsKeyDown(KeyboardKey key) {
            this._bufferAccess.WaitOne();
            var sfmlKey = key.ToSFML();
            var isKeyDown = sfmlKey != Keyboard.Key.Unknown ? Keyboard.IsKeyPressed(sfmlKey) : false;
            this._bufferAccess.ReleaseMutex();
            return isKeyDown;
        }

        public override Camera GetCamera() {
            return this._camera;
        }

        public override void Destroy() {
            this._buffer.Close();
        }

        public override Data.Shared.Vector GetRealMousePos() {
            this._bufferAccess.WaitOne();
            var realpos = Mouse.GetPosition(this._buffer);
            var pos = this._buffer.MapPixelToCoords(realpos, this._uiView);
            var realMousePos = Data.Shared.Vector.Create(pos.X, pos.Y);
            this._bufferAccess.ReleaseMutex();
            return realMousePos;
        }

        public override Data.Shared.Vector GetGameWorldMousePos() {
            this._bufferAccess.WaitOne();
            var mousePos = Mouse.GetPosition(this._buffer);
            var gamePos = this._buffer.MapPixelToCoords(mousePos, this._gameContentView);
            var gameWorldMousePos = Data.Shared.Vector.Create(gamePos.X, gamePos.Y);
            this._bufferAccess.ReleaseMutex();
            return gameWorldMousePos;
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

        public override bool IsJoystickConnected(uint joystickId) {
            this._bufferAccess.WaitOne();
            var isConnected = Joystick.IsConnected(joystickId);
            this._bufferAccess.ReleaseMutex();
            return isConnected;
        }

        public override bool IsJoystickButtonPressed(uint joystickId, JoystickButton button) {
            this._bufferAccess.WaitOne();
            var isPressed = Joystick.IsButtonPressed(joystickId, (uint)button);
            this._bufferAccess.ReleaseMutex();
            return isPressed;
        }

        public override float GetJoystickAxis(uint joystickId, JoystickAxis axis) {
            this._bufferAccess.WaitOne();
            var position = Joystick.GetAxisPosition(joystickId, (Joystick.Axis)axis);
            this._bufferAccess.ReleaseMutex();
            return position;
        }

        public override void ProcessEvents() {
            this._bufferAccess.WaitOne();
            this._buffer.DispatchEvents();
            Joystick.Update();
            this._bufferAccess.ReleaseMutex();
        }

        public override void ChangeResolution(uint width, uint height) {
            this.ModifyBuffer(width, height, this._style);
        }

        public override Data.Shared.Vector GetResolution() {
            return this._resolution;
        }
    }
}
