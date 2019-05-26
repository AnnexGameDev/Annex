using Annex.Data;

namespace Annex.Graphics.Contexts
{
    public class TextAlignment
    {
        public Vector2f Size;
        public HorizontalAlignment HorizontalAlignment;
        public VerticalAlignment VerticalAlignment;

        public TextAlignment() {
            this.Size = new Vector2f();
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
