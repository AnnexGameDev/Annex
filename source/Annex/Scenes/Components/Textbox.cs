using Annex.Data.Shared;
using Annex.Graphics;
using Annex.Graphics.Contexts;
using Annex.Graphics.Events;

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

        public override void Draw(ICanvas canvas) {
            base.Draw(canvas);
            canvas.Draw(this.RenderText);
        }

        public override void HandleKeyboardKeyPressed(KeyboardKeyPressedEvent e) {
            e.Handled = true;
            if (e.Key == KeyboardKey.BackSpace) {
                if (System.String.IsNullOrEmpty(this.Text.Value)) {
                    return;
                }
                this.Text.Set(this.Text.Value[0..^1]);
                return;
            }
            if (e.ToString().Length == 1) {
                this.Text.Set(this.Text.Value + e.ToString());
            }
        }
    }
}
