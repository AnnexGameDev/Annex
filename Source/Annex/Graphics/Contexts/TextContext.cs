using Annex.Data;
using Annex.Data.ReferenceTypes;

namespace Annex.Graphics.Contexts
{
    public class TextContext
    {
        public PString RenderText { get; set; }

        public PVector RenderPosition { get; set; }
        public TextAlignment? Alignment { get; set; }

        public PString FontName { get; set; }
        public uint FontSize { get; set; }
        public RGBA FontColor { get; set; }

        public float BorderThickness { get; set; }
        public RGBA BorderColor { get; set; }

        public bool IsAbsolute { get; set; }

        public TextContext(PString text, PString font) {
            this.RenderText = text;

            this.RenderPosition = new PVector();
            this.Alignment = null;

            this.FontName = font;
            this.FontSize = 12;
            this.FontColor = new RGBA();

            this.BorderThickness = 0;
            this.BorderColor = new RGBA();
        }
    }
}
