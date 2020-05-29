using Annex.Data.Shared;
using Annex.Graphics;
using Annex.Graphics.Contexts;

namespace Annex.Scenes.Components
{
    public class Label : Image
    {
        protected readonly TextContext RenderText;
        public readonly String Caption;
        public readonly String Font;
        public readonly Int FontSize;

        public Label(string elementID = "") : base(elementID) {
            this.Caption = new String();
            this.Font = new String();
            this.FontSize = new Int(12);

            this.RenderText = new TextContext(this.Caption, this.Font) {
                RenderPosition = this.Position,
                UseUIView = true,
                Alignment = new TextAlignment() {
                    Size = this.Size,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Middle
                },
                FontSize = this.FontSize
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
