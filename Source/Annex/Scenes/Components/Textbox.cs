using Annex.Data.Shared;
using Annex.Graphics;
using Annex.Graphics.Contexts;

namespace Annex.Scenes.Components
{
    public class Textbox : Image
    {
        protected readonly TextContext RenderText;
        public readonly String Text;
        public readonly String Font;

        public Textbox(string elementID = "") : base(elementID) {
            this.Text = new String();
            this.Font = new String();

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

        public override void Draw(IDrawableContext context) {
            base.Draw(context);
            context.Draw(this.RenderText);
        }

        public override void HandleKeyboardKeyPressed(KeyboardKey key) {
            if (key == KeyboardKey.BackSpace) {
                if (System.String.IsNullOrEmpty(this.Text.Value)) {
                    return;
                }
                this.Text.Set(this.Text.Value.Substring(0, this.Text.Value.Length - 1));
                return;
            }
            if (key.ToString().Length == 1) {
                this.Text.Set(this.Text.Value + key.ToString());
            }
        }
    }
}
