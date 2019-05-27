using Annex.Data.ReferenceTypes;

namespace Annex.Graphics.Contexts
{
    public class TextAlignment
    {
        public PVector Size;
        public HorizontalAlignment HorizontalAlignment;
        public VerticalAlignment VerticalAlignment;

        public TextAlignment() {
            this.Size = new PVector();
        }
    }

    public enum HorizontalAlignment
    {
        Left,
        Center,
        Right
    }

    public enum VerticalAlignment
    {
        Top,
        Middle,
        Bottom
    }
}
