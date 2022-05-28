using Annex_Old.Data.Shared;
using Annex_Old.Graphics;
using Annex_Old.Graphics.Contexts;

namespace Annex_Old.Scenes.Components
{
    public class Passwordbox : Textbox
    {
        protected readonly TextContext _passwordRenderText;
        private String _secureString;
        private char _passwordChar;
        public char PasswordChar {
            get => this._passwordChar;
            set {
                this._passwordChar = value;
                this._secureString.Set(new string(value, this._secureString.Value!.Length));
            }
        }

        public Passwordbox(string elementID = "") : base(elementID) {
            this._secureString = new String("");
            this._passwordRenderText = new TextContext(this._secureString, this.RenderText.FontName)
            {
                RenderPosition = this.RenderText.RenderPosition,
                FontSize = this.RenderText.FontSize,
                FontColor = this.RenderText.FontColor,
                Alignment = this.RenderText.Alignment,
                UseUIView = true
            };
        }

        public override void Draw(ICanvas canvas) {
            if (!this.Visible) {
                return;
            }

            if (this.Text.Value?.Length != this._secureString.Value?.Length) {
                this._secureString.Set(new string(this.PasswordChar, this.Text.Value?.Length ?? 0));
            }

            canvas.Draw(this.ImageContext);
            canvas.Draw(this._passwordRenderText);
        }
    }
}
