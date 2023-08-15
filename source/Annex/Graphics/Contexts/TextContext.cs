using Annex_Old.Data;
using Annex_Old.Data.Shared;

namespace Annex_Old.Graphics.Contexts
{
    public class TextContext : DrawingContext
    {
        public String RenderText { get; set; }

        public Vector RenderPosition { get; set; }
        public TextAlignment? Alignment { get; set; }

        public String FontName { get; set; }
        public Int FontSize { get; set; }
        public RGBA FontColor { get; set; }

        public float BorderThickness { get; set; }
        public RGBA BorderColor { get; set; }

        public TextContext(String text, String font) {
            this.RenderText = text;

            this.RenderPosition = Vector.Create();
            this.Alignment = null;

            this.FontName = font;
            this.FontSize = new Int(12);
            this.FontColor = new RGBA();

            this.BorderThickness = 0;
            this.BorderColor = new RGBA();
        }
    }
}
