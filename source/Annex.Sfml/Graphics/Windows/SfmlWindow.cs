﻿using Annex.Core.Data;
using Annex.Core.Events.Core;
using Annex.Core.Graphics.Windows;
using Annex.Core.Input;
using Annex.Sfml.Extensions;
using SFML.Graphics;
using SFML.Window;

namespace Annex.Sfml.Graphics.Windows
{
    internal class SfmlWindow : IWindow, IDisposable
    {
        private readonly IInputHandlerService _inputHandlerService;
        private RenderWindow? _renderWindow;

        public Vector2ui WindowResolution { get; }
        public Vector2ui WindowSize { get; }
        public Vector2i WindowPosition { get; }

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

        public SfmlWindow(ICoreEventService coreEventService, IInputHandlerService inputHandlerService) {
            this._inputHandlerService = inputHandlerService;
            this.WindowSize = new Vector2ui(OnWindowSizeChanged);
            this.WindowPosition = new Vector2i(OnWindowPositionChanged);
            this.WindowResolution = new Vector2ui(OnWindowResolutionChanged);
            this.CreateWindow();

            coreEventService.Add(CoreEventType.Graphics, new DrawGameEvent(this));
            coreEventService.Add(CoreEventType.UserInput, new DoEvents(this));

        }

        private void OnWindowResolutionChanged() {
            if (this.WindowResolution != null)
                this.ReCreateWindow();
        }

        private void OnWindowSizeChanged() {
            this._renderWindow?.Size.Set(this.WindowSize);
        }

        private void OnWindowPositionChanged() {
            this._renderWindow?.Position.Set(this.WindowPosition);
        }

        private void ReCreateWindow() {
            this.DestroyWindow();
            this.CreateWindow();
        }

        private void CreateWindow() {
            var videoMode = new VideoMode(this.WindowResolution.X, this.WindowResolution.Y);
            this._renderWindow = new RenderWindow(videoMode, this.Title, this.WindowStyle.ToSfmlStyle());

            this._renderWindow.Size.Set(this.WindowSize);
            this._renderWindow.Position.Set(this.WindowPosition);
            this._renderWindow.SetVisible(this.IsVisible);

            this.AttachInputHandlers();
        }

        private void DestroyWindow() {
            this.RemoveInputHandlers();
            this._renderWindow?.Dispose();
            this._renderWindow = null;
        }

        public void Dispose() {
            this.DestroyWindow();
        }

        private class DrawGameEvent : Core.Events.Event
        {
            private readonly SfmlWindow _sfmlWindow;

            public DrawGameEvent(SfmlWindow sfmlWindow) : base(16, 0) {
                this._sfmlWindow = sfmlWindow;
            }

            protected override void Run() {
                if (this._sfmlWindow._renderWindow is RenderWindow buffer) {
                    buffer.Clear();
                    buffer.Display();
                }
            }
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
        #endregion

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