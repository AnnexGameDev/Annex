using Annex.Core.Assets;
using Annex.Core.Data;
using Annex.Core.Events.Core;
using Annex.Core.Graphics;
using Annex.Core.Graphics.Windows;
using Annex.Core.Input;
using Annex.Core.Scenes;
using Annex.Sfml.Collections.Generic;
using Annex.Sfml.Extensions;
using Annex.Sfml.Graphics.PlatformTargets;
using SFML.Graphics;
using SFML.Window;

namespace Annex.Sfml.Graphics.Windows
{
    internal class SfmlWindow : SfmlCanvas, IWindow, IDisposable
    {
        private readonly IInputService _inputHandlerService;
        private RenderWindow? _renderWindow;
        protected override RenderTarget? _renderTarget => _renderWindow;

        private Vector2f _windowResolution = new();
        public IVector2<float> WindowResolution => this._windowResolution;

        private Vector2f _windowSize = new();
        public IVector2<float> WindowSize => this._windowSize;

        private Vector2f _windowPosition = new();
        public IVector2<float> WindowPosition => this._windowPosition;

        private string _title = string.Empty;
        public string Title
        {
            get => this._title;
            set
            {
                this._title = value;
                this._renderWindow?.SetTitle(this.Title);
            }
        }

        private bool _isVisible = false;
        public bool IsVisible
        {
            get => this._isVisible;
            set
            {
                this._isVisible = value;
                this._renderWindow?.SetVisible(this.IsVisible);
            }
        }

        private WindowStyle _windowStyle = WindowStyle.Default;

        public WindowStyle WindowStyle
        {
            get => this._windowStyle;
            set
            {
                this._windowStyle = value;
                this.ReCreateWindow();
            }
        }

        public SfmlWindow(ICoreEventService coreEventService, IInputService inputHandlerService, ISceneService sceneService, IPlatformTargetFactory platformTargetFactory, ICameraCache cameraCache)
            : base(platformTargetFactory, cameraCache) {
            this._inputHandlerService = inputHandlerService;
            this.CreateWindow();
            
            coreEventService.Add(CoreEventPriority.Graphics, new DrawGameEvent(this, sceneService));
            coreEventService.Add(CoreEventPriority.UserInput, new DoEvents(this));

            var defaultCamera = new Camera(CameraId.Default) {
                Region = new Core.Data.FloatRect(0, 0, 1, 1),
                Size = this.WindowResolution,
                Center = new ScalingVector2f(this.WindowResolution, 0.5f, 0.5f),
            };  
            this.AddCamera(defaultCamera);

            var uiCamera = new Camera(CameraId.UI) {
                Region = new Core.Data.FloatRect(0, 0, 1, 1),
                Size = this.WindowResolution,
                Center = new ScalingVector2f(this.WindowResolution, 0.5f, 0.5f),
            };
            this.AddCamera(uiCamera);
        }

        private void ReCreateWindow() {
            this.DestroyWindow();
            this.CreateWindow();
        }

        private void CreateWindow() {
            var videoMode = new VideoMode((uint)this.WindowResolution.X, (uint)this.WindowResolution.Y);
            this._renderWindow = new RenderWindow(videoMode, this.Title, this.WindowStyle.ToSfmlStyle());

            this._renderWindow.Size.Set(this.WindowSize);
            this._renderWindow.Position.Set(this.WindowPosition);
            this._renderWindow.SetVisible(this.IsVisible);

            this.AttachInputHandlers();
        }

        private void DestroyWindow() {
            this.RemoveInputHandlers();
            this._renderWindow?.Dispose();
            this._renderWindow = default;
        }

        public void Dispose() {
            this.DestroyWindow();
        }

        #region Input
        private void AttachInputHandlers() {
            if (this._renderWindow != null) {
                this._renderWindow.KeyPressed += OnKeyboardKeyPressed;
                this._renderWindow.KeyReleased += OnKeyboardKeyReleased;
                this._renderWindow.Closed += OnWindowClosed;
                this._renderWindow.MouseButtonPressed += OnMouseButtonPressed;
                this._renderWindow.MouseButtonReleased += OnMouseButtonReleased;
                this._renderWindow.MouseWheelScrolled += OnMouseScrollWheelMoved;
                this._renderWindow.MouseMoved += OnMouseMoved;
            }
        }

        private void RemoveInputHandlers() {
            if (this._renderWindow != null) {
                this._renderWindow.KeyPressed -= OnKeyboardKeyPressed;
                this._renderWindow.KeyReleased -= OnKeyboardKeyReleased;
                this._renderWindow.Closed -= OnWindowClosed;
                this._renderWindow.MouseButtonPressed -= OnMouseButtonPressed;
                this._renderWindow.MouseButtonReleased -= OnMouseButtonReleased;
                this._renderWindow.MouseWheelScrolled -= OnMouseScrollWheelMoved;
                this._renderWindow.MouseMoved -= OnMouseMoved;
            }
        }

        private void OnKeyboardKeyPressed(object? sender, KeyEventArgs e) => this._inputHandlerService?.HandleKeyboardKeyPressed(this, e.Code.ToKeyboardKey());
        private void OnKeyboardKeyReleased(object? sender, KeyEventArgs e) => this._inputHandlerService?.HandleKeyboardKeyReleased(this, e.Code.ToKeyboardKey());
        private void OnWindowClosed(object? sender, EventArgs e) => this._inputHandlerService?.HandleWindowClosed(this);

        private void OnMouseMoved(object? sender, MouseMoveEventArgs e) => this._inputHandlerService?.HandleMouseMoved(this, e.X, e.Y);
        private void OnMouseScrollWheelMoved(object? sender, MouseWheelScrollEventArgs e) => this._inputHandlerService?.HandleMouseScrollWheelMoved(this, e.Delta);
        private void OnMouseButtonReleased(object? sender, MouseButtonEventArgs e) => this._inputHandlerService?.HandleMouseButtonReleased(this, e.Button.ToMouseButton(), e.X, e.Y);
        private void OnMouseButtonPressed(object? sender, MouseButtonEventArgs e) => this._inputHandlerService?.HandleMouseButtonPressed(this, e.Button.ToMouseButton(), e.X, e.Y);

        public bool IsKeyDown(KeyboardKey key) {
            if (this._renderWindow?.HasFocus() != true)
                return false;

            return Keyboard.IsKeyPressed(key.ToSfmlKeyboardKey());
        }
        #endregion

        public Camera? GetCamera(CameraId cameraId) => GetCamera(cameraId.ToString());

        public Camera? GetCamera(string cameraId) {
            return this.CameraCache.GetCamera(cameraId)?.Camera;
        }

        public void AddCamera(Camera camera) {
            this.CameraCache.AddCamera(camera);
        }

        public void SetIcon(uint sizeX, uint sizeY, IAsset icon) {
            using var image = new Image(icon.ToBytes());
            this._renderWindow?.SetIcon(sizeX, sizeY, image.Pixels);
        }

        public void SetMouseImage(IAsset img, uint sizeX, uint sizeY, uint offsetX, uint offsetY) {
            using var image = new Image(img.ToBytes());
            this._renderWindow?.SetMouseCursor(new Cursor(image.Pixels, new SFML.System.Vector2u(sizeX, sizeY), new SFML.System.Vector2u(offsetX, offsetY)));
        }

        public void SetResolution(float resolutionX, float resolutionY) {
            this._windowResolution.Set(resolutionX, resolutionY);
            this.ReCreateWindow();
        }

        public void SetResolution(IVector2<float> newResolution) {
            this.SetResolution(newResolution.X, newResolution.Y);
        }

        public void SetSize(float sizeX, float sizeY) {
            this._windowSize.Set(sizeX, sizeY);
            this._renderWindow?.Size.Set(this.WindowSize);
        }

        public void SetSize(IVector2<float> newSize) {
            this.SetSize(newSize.X, newSize.Y);
        }

        public void SetPosition(float x, float y) {
            this._windowPosition.Set(x, y);
            this._renderWindow?.Position.Set(this.WindowPosition);
        }

        public void SetPosition(IVector2<float> newPosition) {
            this.SetPosition(newPosition.X, newPosition.Y);
        }

        private class DrawGameEvent : Core.Events.Event
        {
            private readonly SfmlWindow _sfmlWindow;
            private readonly ISceneService _sceneService;

            public DrawGameEvent(SfmlWindow sfmlWindow, ISceneService sceneService) : base(16, 0) {
                this._sfmlWindow = sfmlWindow;
                this._sceneService = sceneService;
            }

            protected override void Run() {
                if (this._sfmlWindow._renderWindow is RenderWindow buffer) {
                    buffer.Clear();
                    this._sceneService.CurrentScene?.Draw(this._sfmlWindow);
                    buffer.Display();
                }
            }
        }

        private class DoEvents : Core.Events.Event
        {
            private readonly SfmlWindow _sfmlWindow;

            public DoEvents(SfmlWindow sfmlWindow) : base(16, 0) {
                this._sfmlWindow = sfmlWindow;
            }

            protected override void Run() {
                if (this._sfmlWindow._renderWindow is RenderWindow buffer) {
                    buffer.DispatchEvents();
                }
            }
        }
    }
}