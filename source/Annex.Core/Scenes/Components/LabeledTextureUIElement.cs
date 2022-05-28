using Annex_Old.Core.Data;
using Annex_Old.Core.Graphics;
using Annex_Old.Core.Graphics.Contexts;

namespace Annex_Old.Core.Scenes.Components
{
    public abstract class LabeledTextureUIElement : UIElement, IButton, ILabel
    {
        protected readonly Image Image;
        protected readonly Label Label;

        public string BackgroundTextureId
        {
            get => this.Image.BackgroundTextureId;
            set => this.Image.BackgroundTextureId = value;
        }
        public string Text
        {
            get => this.Label.Text;
            set => this.Label.Text = value;
        }
        public string Font
        {
            get => this.Label.Font;
            set => this.Label.Font = value;
        }
        public uint FontSize
        {
            get => this.Label.FontSize;
            set => this.Label.FontSize = value;
        }
        public RGBA FontColor
        {
            get => this.Label.FontColor;
            set => this.Label.FontColor = value;
        }
        public HorizontalAlignment HorizontalTextAlignment
        {
            get => this.Label.HorizontalTextAlignment;
            set => this.Label.HorizontalTextAlignment = value;
        }
        public VerticalAlignment VerticalTextAlignment
        {
            get => this.Label.VerticalTextAlignment;
            set => this.Label.VerticalTextAlignment = value;
        }
        public IVector2<float> TextPositionOffset
        {
            get => this.Label.TextPositionOffset;
            set => this.Label.TextPositionOffset = value;
        }

        public LabeledTextureUIElement(string? elementId = null, IVector2<float>? position = null, IVector2<float>? size = null) : base(elementId, position, size) {

            this.Image = new Image($"{elementId}.background", this.Position, this.Size);
            this.Label = new Label($"{elementId}.label", this.Position, this.Size);
        }

        protected override void DrawInternal(ICanvas canvas) {
            this.Image.Draw(canvas);
            this.Label.Draw(canvas);
        }
    }
}
