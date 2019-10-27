using Annex.Data.Shared;

namespace Annex.Graphics.Contexts
{
    public class TextAlignment
    {
        public Vector Size;
        public HorizontalAlignment HorizontalAlignment;
        public VerticalAlignment VerticalAlignment;

        public TextAlignment() {
            this.Size = Vector.Create(0, 0);
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
