using Annex_Old.Graphics.Events;

namespace Annex_Old.Scenes.Components
{
    public class Textbox : Label
    {
        public Textbox(string elementID = "") : base(elementID) {
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
            this.Text.Set(this.Text.Value + e.LiteralContent);
        }
    }
}
