using Annex.Core.Data;
using Annex.Core.Graphics;
using Annex.Core.Helpers;
using Annex.Core.Input.InputEvents;

namespace Annex.Core.Scenes.Components
{
    public class PasswordBox : TextBox, IPasswordBox
    {
        public char PasswordChar { get; set; } = '*';

        public PasswordBox(string? elementId = null, IVector2<float>? position = null, IVector2<float>? size = null) : base(elementId, position, size) {
        }

        protected override void DrawInternal(ICanvas canvas) {
            // Basically just a hack. Swap out the text each render so the logic still holds, but we prevent 
            // the actual text from being read
            string oldText = this.Text;
            this.Text = new string(this.PasswordChar, this.Text.Length);
            base.DrawInternal(canvas);
            this.Text = oldText;
        }

        public override void OnMouseButtonReleased(MouseButtonReleasedEvent mouseButtonReleasedEvent) {

            // Prevent copying/pasting
            if (mouseButtonReleasedEvent.Button == Input.MouseButton.Right) {
                return;
            }

            base.OnMouseButtonReleased(mouseButtonReleasedEvent);
        }

        public override void OnKeyboardKeyPressed(KeyboardKeyPressedEvent keyboardKeyPressedEvent) {

            // Prevent copying/pasting
            if (KeyboardHelper.IsControlPressed() && 
                (keyboardKeyPressedEvent.Key == Input.KeyboardKey.C || keyboardKeyPressedEvent.Key == Input.KeyboardKey.X)) {
                return;
            }

            base.OnKeyboardKeyPressed(keyboardKeyPressedEvent);
        }
    }
}
