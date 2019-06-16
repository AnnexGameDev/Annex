using Annex.Data.Binding;
using Annex.Graphics;
using Annex.Graphics.Contexts;

namespace Annex.UserInterface.Components
{
    public class Textbox : RenderBoxElement
    {
        protected readonly TextContext RenderText;
        public readonly PString Text;
        public readonly PString Font;

        public Textbox(string elementID = "") : base(elementID) {
            this.Text = new PString();
            this.Font = new PString();

            this.RenderText = new TextContext(this.Text, this.Font) {
                RenderPosition = this.Position,
                UseUIView = true,
                Alignment = new TextAlignment() {
                    Size = this.Size,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Middle
                }
            };
        }

        public override void Draw(IDrawableContext surfaceContext) {
            base.Draw(surfaceContext);
            surfaceContext.Draw(this.RenderText);
        }

        public override void HandleKeyboardKeyPressed(KeyboardKey key) {
            if (key == KeyboardKey.BackSpace) {
                if (string.IsNullOrEmpty(this.Text.Value)) {
                    return;
                }
                this.Text.Set(this.Text.Value[0..^1]);
                return;
            }
            if (key.ToString().Length == 1) {
                this.Text.Set(this.Text.Value + key.ToString());
            }
        }
    }
}
