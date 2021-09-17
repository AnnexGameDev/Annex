using Annex.Assets.Converters;
using Annex.Data.Shared;
using Annex.Events;
using Annex.Graphics.Cameras;
using Annex.Graphics.Contexts;
using Annex.Graphics.Events;
using Annex.Graphics.Sfml.Assets;
using Annex.Graphics.Sfml.Events;
using Annex.Graphics.Sfml.Targets;
using Annex.Scenes;
using Annex.Services;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Threading;
using static Annex.Graphics.Sfml.Errors;

namespace Annex.Graphics.Sfml
{
    public class SfmlCanvas : ICanvas
    {
        public bool IsActive { get => this._buffer?.HasFocus() == true; }

        private Mutex _bufferAccess;
        private bool _usingUiView;
        private readonly View _uiView;
        private readonly View _gameContentView;
        private readonly Camera _camera;
        private RenderWindow _buffer;

        private readonly Vector _resolution;
        private string _title { get; set; }
        private Styles _style { get; set; }

        private readonly TextureConverter _textureConverter;
        private readonly FontConverter _fontConverter;
        private readonly IconConverter _iconConverter;

        private readonly InputHandler _inputHandler;

        public event System.EventHandler<WindowResizedEvent>? OnWindowResized;

        public SfmlCanvas(WindowMode mode) {
            int resolutionWidth = 960;
            int resolutionHeight = 640;

            this._textureConverter = new TextureConverter();
            this._fontConverter = new FontConverter();
            this._iconConverter = new IconConverter();

            this._bufferAccess = new Mutex();
            this._resolution = Vector.Create(resolutionWidth, resolutionHeight);
            this._camera = new Camera(this._resolution);
            this._uiView = new View(new Vector2f(this._resolution.X / 2, this._resolution.Y / 2), new Vector2f(this._resolution.X, this._resolution.Y));
            this._gameContentView = new View();
            this._buffer = new RenderWindow(new SFML.Window.VideoMode((uint)this._resolution.X, (uint)this._resolution.Y), "", (Styles)mode);
            this._style = Styles.Default;
            this.SetTitle("Window");

            this._inputHandler = new SfmlInputHandler(this._buffer);

            // Has to be handled differently
            this._buffer.Resized += (sender, e) => {
                this.OnWindowResized?.Invoke(this, new WindowResizedEvent(e.Width, e.Height));
            };

            var events = ServiceProvider.EventService;
            events.AddEvent(PriorityType.GRAPHICS, new GraphicsEvent(this, this.BeginDrawing, this.EndDrawing)); ;
            events.AddEvent(PriorityType.INPUT, new InputEvent(this.ProcessEvents));
        }

        public void SetWindowIcon(string iconPath) {
            var args = new AssetConverterArgs(iconPath, this._iconConverter);
            ServiceProvider.IconManager.GetAsset(args, out var resource);
            var icon = (Image)resource.GetTarget();
            this._buffer.SetIcon(icon.Size.X, icon.Size.Y, icon.Pixels);
        }

        public void SetTitle(string title) {
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
            this._bufferAccess.ReleaseMutex();
        }

        public void Draw(TextContext ctx) {

            if (System.String.IsNullOrEmpty(ctx.RenderText)) {
                return;
            }

            // We need to update the camera.
            this.UpdateView(ctx);

            var args = new AssetConverterArgs(ctx.FontName.Value, this._fontConverter);
            ServiceProvider.FontManager.GetAsset(args, out var asset);
            var font = (Font)asset.GetTarget();
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

            if (sheet.Target is not SheetPlatformTarget) {
                sheet.Target?.Dispose();
                sheet.Target = new SheetPlatformTarget();
            }
            var target = (SheetPlatformTarget)sheet.Target;


            if (target.TextureId != sheet.SourceTextureName.Value) {
                target.TextureId = sheet.SourceTextureName.Value;

                var rect = sheet.SourceTextureRect;
                if (rect.Width == SpriteSheetContext.DETERMINE_SIZE_FROM_IMAGE && rect.Height == SpriteSheetContext.DETERMINE_SIZE_FROM_IMAGE) {
                    var args = new AssetConverterArgs(sheet.SourceTextureName.Value, this._textureConverter);
                    bool loadSuccess = ServiceProvider.TextureManager.GetAsset(args, out var asset);
                    Debug.Assert(loadSuccess, TEXTURE_FAILED_TO_LOAD.Format(sheet.SourceTextureName.Value));
                    using var sprite = new Sprite((Texture)asset.GetTarget());
                    var size = sprite.Texture.Size;

                    int width = (int)(size.X / sheet.NumColumns);
                    int height = (int)(size.Y / sheet.NumRows);

                    rect.Height.Set(height);
                    rect.Width.Set(width);
                }
            }

            this.Draw(sheet._internalTexture);
        }

        public void Draw(TextureContext ctx) {

            if (string.IsNullOrEmpty(ctx.SourceTextureName)) {
                return;
            }

            // We need to update the camera.
            this.UpdateView(ctx);

            if (ctx.Target is not TexturePlatformTarget) {
                ctx.Target?.Dispose();
                ctx.Target = new TexturePlatformTarget();
            }
            var target = (TexturePlatformTarget)ctx.Target;
            var sprite = target.Sprite;

            if (target.TextureName != ctx.SourceTextureName.Value) {
                var args = new AssetConverterArgs(ctx.SourceTextureName.Value!, this._textureConverter);
                bool loadSuccess = ServiceProvider.TextureManager.GetAsset(args, out var asset);
                Debug.Assert(loadSuccess, TEXTURE_FAILED_TO_LOAD.Format(ctx.SourceTextureName.Value!));

                sprite.Texture = (Texture)asset.GetTarget();
                target.TextureName = ctx.SourceTextureName.Value;
            }

            target.RenderPosition.X = ctx.RenderPosition.X;
            target.RenderPosition.Y = ctx.RenderPosition.Y;
            sprite.Position = target.RenderPosition;

            var renderSize = target.RenderSize;
            if (ctx.RenderSize == null) {
                if (ctx.SourceTextureRect == null) {
                    renderSize.X = sprite.Texture.Size.X;
                    renderSize.Y = sprite.Texture.Size.Y;
                } else {
                    renderSize.X = ctx.SourceTextureRect.Width;
                    renderSize.Y = ctx.SourceTextureRect.Height;
                }
            } else {
                renderSize.X = ctx.RenderSize.X;
                renderSize.Y = ctx.RenderSize.Y;
            }

            if (ctx.SourceTextureRect != null) {
                sprite.TextureRect = ctx.SourceTextureRect;
                target.Scale.X = renderSize.X / ctx.SourceTextureRect.Width;
                target.Scale.Y = renderSize.Y / ctx.SourceTextureRect.Height;
            } else {
                target.Scale.X = renderSize.X / sprite.Texture.Size.X;
                target.Scale.Y = renderSize.Y / sprite.Texture.Size.Y;
            }
            sprite.Scale = target.Scale;

            if (ctx.RenderColor != null) {
                sprite.Color = ctx.RenderColor;
            }

            if (ctx.Rotation % 360 != 0) {

                target.Origin.X = ctx.RelativeRotationOrigin.X;
                target.Origin.Y = ctx.RelativeRotationOrigin.Y;

                sprite.Origin = target.Origin;
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
    }
}
