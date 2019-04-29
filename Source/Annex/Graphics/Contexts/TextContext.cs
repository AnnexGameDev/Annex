namespace Annex.Graphics.Contexts
{
    public class TextContext
    {
        public string Text { get; set; }
        public string Font { get; set; }

        public TextContext(string text, string font) {
            this.Text = text;
            this.Font = font;
        }
    }
}
