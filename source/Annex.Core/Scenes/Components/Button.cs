using Annex_Old.Core.Data;
using Annex_Old.Core.Graphics;
using Annex_Old.Core.Graphics.Contexts;

namespace Annex_Old.Core.Scenes.Components
{
    public class Button : UIElement, IButton
    {
        private readonly Image _background;
        private readonly Label _label;

        public string BackgroundTextureId
        {
            get => this._background.BackgroundTextureId;
            set => this._background.BackgroundTextureId = value;
        }
        public string Text
        {
            get => this._label.Text;
            set => this._label.Text = value;
        }
        public string Font
        {
            get => this._label.Font;
            set => this._label.Font = value;
        }
        public uint FontSize
        {
            get => this._label.FontSize;
            set => this._label.FontSize = value;
        }
        public RGBA FontColor
        {
            get => this._label.FontColor;
            set => this._label.FontColor = value;
        }
        public HorizontalAlignment HorizontalTextAlignment
        {
            get => this._label.HorizontalTextAlignment;
            set => this._label.HorizontalTextAlignment = value;
        }
        public VerticalAlignment VerticalTextAlignment
        {
            get => this._label.VerticalTextAlignment;
            set => this._label.VerticalTextAlignment = value;
        }
        public IVector2<float> TextPositionOffset
        {
            get => this._label.TextPositionOffset;
            set => this._label.TextPositionOffset = value;
        }

        public Button(string? elementId = null, IVector2<float>? position = null, IVector2<float>? size = null) : base(elementId, position, size) {

            this._background = new Image($"{elementId}.background", this.Position, this.Size);
            this._label = new Label($"{elementId}.label", this.Position, this.Size);
        }

        protected override void DrawInternal(ICanvas canvas) {
            this._background.Draw(canvas);
            this._label.Draw(canvas);
        }
    }
}
