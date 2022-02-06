using Annex.Core.Data;
using Annex.Core.Graphics.Windows;
using Annex.Sfml.Extensions;
using SFML.Graphics;
using SFML.Window;

namespace Annex.Sfml.Graphics.Windows
{
    internal class SfmlWindow : IWindow
    {
        private readonly RenderWindow _renderWindow;

        public SfmlWindow(WindowStyle style) {
            var videoMode = new VideoMode();
            this._renderWindow = new RenderWindow(videoMode, this.Title, style.ToSfmlStyle());
        }

        public Vector2ui Size {
            get => this._renderWindow.Size.ToAnnexVector();
            set => this._renderWindow.Size.Set(value.ToSfmlVector());
        }
        public Vector2i Position {
            get => this._renderWindow.Position.ToAnnexVector();
            set => this._renderWindow.Position.Set(value.ToSfmlVector());
        }

        private string _title = string.Empty;
        public string Title {
            get => this._title;
            set
            {
                this._title = value;
                this._renderWindow.SetTitle(this.Title);
            }
        }

        private bool _isVisible = false;
        public bool IsVisible
        {
            get => this._isVisible;
            set
            {
                this._isVisible = value;
                this._renderWindow.SetVisible(this.IsVisible);
            }
        }
    }
}