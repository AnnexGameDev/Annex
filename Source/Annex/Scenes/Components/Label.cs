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

        public Label(string elementID = "") : base(elementID) {
            this.Caption = new String();
            this.Font = new String();

            this.RenderText = new TextContext(this.Caption, this.Font) {
                RenderPosition = this.Position,
                UseUIView = true,
                Alignment = new TextAlignment() {
                    Size = this.Size,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Middle
                }
            };
        }

        public override void Draw(IDrawableContext surfaceContext) {
            base.Draw(surfaceContext);
            surfaceContext.Draw(this.RenderText);
        }
    }
}
