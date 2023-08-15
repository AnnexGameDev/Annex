using Annex_Old.Data;
using Annex_Old.Data.Shared;
using Annex_Old.Graphics;
using Annex_Old.Graphics.Contexts;

namespace Annex_Old.Scenes.Components
{
    public class Label : Image
    {
        protected readonly TextContext RenderText;
        public readonly String Text;
        public readonly String Font;
        public readonly Int FontSize;
        public readonly RGBA FontColor;
        public readonly TextAlignment TextAlignment;

        public Label(string elementID = "") : base(elementID) {
            this.Text = new String();
            this.Font = new String();
            this.FontSize = new Int(12);
            this.FontColor = RGBA.Black;
            this.TextAlignment = new TextAlignment() {
                Size = this.Size,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Middle
            };

            this.RenderText = new TextContext(this.Text, this.Font) {
                RenderPosition = this.Position,
                UseUIView = true,
                Alignment = this.TextAlignment,
                FontSize = this.FontSize,
                FontColor = this.FontColor,
            };
        }

        public override void Draw(ICanvas canvas) {
            if (!this.Visible) {
                return;
            }
            base.Draw(canvas);
            canvas.Draw(this.RenderText);
        }
    }
}
