using Annex.Data.Binding;
using Annex.Graphics;
using Annex.Graphics.Contexts;

namespace Annex.UserInterface.Components
{
    public class Button : RenderBoxElement
    {
        protected readonly TextContext RenderText;
        public readonly PString Caption;
        public readonly PString Font;

        public Button(string elementID = "") : base(elementID) {
            this.Caption = new PString();
            this.Font = new PString();

            this.RenderText = new TextContext(this.Caption, this.Font) {
                RenderPosition = this.Position,
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
