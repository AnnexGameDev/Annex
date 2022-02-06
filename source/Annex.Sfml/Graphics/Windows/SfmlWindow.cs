﻿using Annex.Core.Data;
using Annex.Core.Graphics.Windows;
using Annex.Sfml.Extensions;
using SFML.Graphics;
using SFML.Window;

namespace Annex.Sfml.Graphics.Windows
{
    internal class SfmlWindow : IWindow, IDisposable
    {
        private RenderWindow? _renderWindow;

        public Vector2ui WindowResolution { get; }
        public Vector2ui WindowSize { get; }
        public Vector2i WindowPosition { get; }

        private string _title = string.Empty;
        public string Title {
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
        public WindowStyle WindowStyle {
            get => this._windowStyle;
            set {
                this._windowStyle = value;
                this.ReCreateWindow();
            }
        }

        public SfmlWindow() {
            this.WindowSize = new Vector2ui(OnWindowSizeChanged);
            this.WindowPosition = new Vector2i(OnWindowPositionChanged);
            this.WindowResolution = new Vector2ui(OnWindowResolutionChanged);
            this.CreateWindow();
        }

        private void OnWindowResolutionChanged() {
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
        }

        private void DestroyWindow() {
            this._renderWindow?.Dispose();
            this._renderWindow = null;
        }

        public void Dispose() {
            this.DestroyWindow();
        }
    }
}