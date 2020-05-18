using Annex.Assets;
using Annex.Data.Shared;
using Annex.Events;
using Annex.Graphics.Cameras;
using Annex.Graphics.Contexts;
using Annex.Graphics.Events;
using Annex.Scenes;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Threading;
using static Annex.Graphics.Sfml.Errors;
using static Annex.Graphics.EventIDs;

namespace Annex.Graphics.Sfml
{
    public class SfmlCanvas : ICanvas
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

        private readonly Vector _resolution;
        private string _title { get; set; }
        private Styles _style { get; set; }

        public readonly AssetManager FontManager;
        public readonly AssetManager TextureManager;
        public readonly AssetManager IconManager;

        public SfmlCanvas(AssetManager textureManager, AssetManager fontManager, AssetManager iconManager) {
            this.TextureManager = textureManager;
            this.FontManager = fontManager;
            this.IconManager = iconManager;

            int resolutionWidth = 960;
            int resolutionHeight = 640;

            this._bufferAccess = new Mutex();
            this._resolution = Vector.Create(resolutionWidth, resolutionHeight);
            this._camera = new Camera(this._resolution);
            this._uiView = new View(new Vector2f(this._resolution.X / 2, this._resolution.Y / 2), new Vector2f(this._resolution.X, this._resolution.Y));
            this._gameContentView = new View();
            this._buffer = new RenderWindow(new SFML.Window.VideoMode((uint)this._resolution.X, (uint)this._resolution.Y), "", Styles.Default);
            this._style = Styles.Default;
            this.SetTitle("Window");


            var events = ServiceProvider.EventService;
            events.AddEvent(PriorityType.GRAPHICS, (e) => {
                this.BeginDrawing();
                ServiceProvider.SceneService.CurrentScene.Draw(this);
                this.EndDrawing();
            }, 16, 0, DrawGameEventID);
            events.AddEvent(PriorityType.INPUT, (e) => {
                this.ProcessEvents();
            }, 16, 0, ProcessUserInputGameEventID);

            AttachUIHandlers();
        }

        public void SetWindowIcon(string iconPath) {
            var args = new AssetInitializerArgs(iconPath);
            IconManager.GetAsset(args, out var resource);
            var icon = (Image)resource;
            this._buffer.SetIcon(icon.Size.X, icon.Size.Y, icon.Pixels);
        }

        private void SetTitle(string title) {
            this._buffer.SetTitle(title);
            this._title = title;
        }

        private void ModifyBuffer(Vector size, Styles style) {
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

        private void AttachUIHandlers() {
            // Hook up UI events
            this._buffer.Closed += (sender, e) => { ServiceProvider.SceneService.CurrentScene.HandleCloseButtonPressed(); };
            this._buffer.KeyPressed += (sender, e) => {
                ServiceProvider.SceneService.CurrentScene.HandleKeyboardKeyPressed(new KeyboardKeyPressedEvent() {
                    Key = e.Code.ToNonSFML(),
                    ShiftDown = e.Shift
                });
            };
            this._buffer.KeyReleased += (sender, e) => {
                ServiceProvider.SceneService.CurrentScene.HandleKeyboardKeyReleased(new KeyboardKeyReleasedEvent() {
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
                long dt = EventService.CurrentTime - this._lastMouseClick;
                int distanceThreshold = 10;
                int timeThreshold = 250;

                if (Math.Sqrt(dx * dx + dy * dy) < distanceThreshold && dt < timeThreshold) {
                    doubleClick = true;
                }

                this._lastMouseClickX = uiPos.X;
                this._lastMouseClickY = uiPos.Y;
                this._lastMouseClick = EventService.CurrentTime;

                var scene = ServiceProvider.SceneService.CurrentScene;
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
                ServiceProvider.SceneService.CurrentScene.HandleMouseButtonReleased(new MouseButtonReleasedEvent() {
                    Button = e.Button.ToNonSFML(),
                    MouseX = (int)uiPos.X,
                    MouseY = (int)uiPos.Y,
                    WorldX = gamePos.X,
                    WorldY = gamePos.Y,
                    TimeSinceClick = EventService.CurrentTime - this._lastMouseClick
                });
            };
            this._buffer.JoystickButtonPressed += (sender, e) => {
                ServiceProvider.SceneService.CurrentScene.HandleJoystickButtonPressed(new JoystickButtonPressedEvent() {
                    JoystickID = e.JoystickId,
                    Button = (JoystickButton)e.Button
                });
            };
            this._buffer.JoystickButtonReleased += (sender, e) => {
                ServiceProvider.SceneService.CurrentScene.HandleJoystickButtonReleased(new JoystickButtonReleasedEvent() {
                    JoystickID = e.JoystickId,
                    Button = (JoystickButton)e.Button
                });
            };
            this._buffer.JoystickConnected += (sender, e) => {
                ServiceProvider.SceneService.CurrentScene.HandleJoystickConnected(new JoystickConnectedEvent() {
                    JoystickID = e.JoystickId
                });
            };
            this._buffer.JoystickDisconnected += (sender, e) => {
                ServiceProvider.SceneService.CurrentScene.HandleJoystickDisconnected(new JoystickDisconnectedEvent() {
                    JoystickID = e.JoystickId
                });
            };
            this._buffer.JoystickMoved += (sender, e) => {
                ServiceProvider.SceneService.CurrentScene.HandleJoystickMoved(new JoystickMovedEvent() {
                    JoystickID = e.JoystickId,
                    Axis = e.Axis.ToNonSFML(),
                    Delta = e.Position
                });
            };
        }

        public void Draw(TextContext ctx) {

            if (System.String.IsNullOrEmpty(ctx.RenderText)) {
                return;
            }

            // We need to update the camera.
            this.UpdateView(ctx);

            var args = new AssetInitializerArgs(ctx.FontName.Value);
            this.FontManager.GetAsset(args, out var asset);
            var font = (Font)asset;
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

            if (System.String.IsNullOrEmpty(sheet.SourceTextureName)) {
                return;
            }

            if (sheet.SourceTextureRect == null) {
                object? asset = null;
                var args = new AssetInitializerArgs(sheet.SourceTextureName.Value);
                bool loadSuccess = this.TextureManager.GetAsset(args, out asset);
                Debug.Assert(loadSuccess, TEXTURE_FAILED_TO_LOAD.Format(sheet.SourceTextureName.Value));
                using var sprite = new Sprite((Texture)asset);
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

            if (System.String.IsNullOrEmpty(ctx.SourceTextureName)) {
                return;
            }

            // We need to update the camera.
            this.UpdateView(ctx);

            var args = new AssetInitializerArgs(ctx.SourceTextureName.Value);
            bool loadSuccess = this.TextureManager.GetAsset(args, out var asset);
            Debug.Assert(loadSuccess, TEXTURE_FAILED_TO_LOAD.Format(ctx.SourceTextureName.Value));

            using var sprite = new Sprite((Texture)asset) {
                Position = ctx.RenderPosition
            };

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

        private void BeginDrawing() {
            this._bufferAccess.WaitOne();
            this._buffer.Clear();
            this.UpdateGameContentCamera();
        }

        private void EndDrawing() {
            this._buffer.Display();
            this._bufferAccess.ReleaseMutex();
        }

        public void SetVideoMode(VideoMode mode) {
            var style = Styles.Default;

            switch (mode) {
                case VideoMode.FullScreen:
                    style = Styles.Fullscreen;
                    break;
                case VideoMode.Window:
                    style = Styles.Default;
                    break;
                default:
                    Debug.Error(UNKNOWN_VIDEO_MODE.Format(mode));
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

        public void SetVisible(bool visible) {
            this._buffer.SetVisible(visible);
        }

        public bool IsMouseButtonDown(MouseButton button) {
            this._bufferAccess.WaitOne();
            var sfmlButton = button.ToSFML();
            var isMouseButtonDown = Mouse.IsButtonPressed(sfmlButton);
            this._bufferAccess.ReleaseMutex();
            return isMouseButtonDown;
        }

        public bool IsKeyDown(KeyboardKey key) {
            this._bufferAccess.WaitOne();
            var sfmlKey = key.ToSFML();
            var isKeyDown = sfmlKey != Keyboard.Key.Unknown ? Keyboard.IsKeyPressed(sfmlKey) : false;
            this._bufferAccess.ReleaseMutex();
            return isKeyDown;
        }

        public Camera GetCamera() {
            return this._camera;
        }

        public void Destroy() {
            this._buffer.Close();
        }

        public Vector GetRealMousePos() {
            this._bufferAccess.WaitOne();
            var realpos = Mouse.GetPosition(this._buffer);
            var pos = this._buffer.MapPixelToCoords(realpos, this._uiView);
            var realMousePos = Data.Shared.Vector.Create(pos.X, pos.Y);
            this._bufferAccess.ReleaseMutex();
            return realMousePos;
        }

        public Vector GetGameWorldMousePos() {
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

        public bool IsJoystickConnected(uint joystickId) {
            this._bufferAccess.WaitOne();
            var isConnected = Joystick.IsConnected(joystickId);
            this._bufferAccess.ReleaseMutex();
            return isConnected;
        }

        public bool IsJoystickButtonPressed(uint joystickId, JoystickButton button) {
            this._bufferAccess.WaitOne();
            var isPressed = Joystick.IsButtonPressed(joystickId, (uint)button);
            this._bufferAccess.ReleaseMutex();
            return isPressed;
        }

        public float GetJoystickAxis(uint joystickId, JoystickAxis axis) {
            this._bufferAccess.WaitOne();
            var position = Joystick.GetAxisPosition(joystickId, (Joystick.Axis)axis);
            this._bufferAccess.ReleaseMutex();
            return position;
        }

        private void ProcessEvents() {
            this._bufferAccess.WaitOne();
            this._buffer.DispatchEvents();
            Joystick.Update();
            this._bufferAccess.ReleaseMutex();
        }

        public void ChangeResolution(uint width, uint height) {
            this.ModifyBuffer(width, height, this._style);
        }

        public Vector GetResolution() {
            return this._resolution;
        }

        public IEnumerable<IAssetManager> GetAssetManagers() {
            yield return this.FontManager;
            yield return this.TextureManager;
            yield return this.IconManager;
        }
    }
}
