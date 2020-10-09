using Annex.Data.Shared;
using System;

namespace Annex.Graphics.Contexts
{
    public class TextAlignment
    {
        public Vector Size;
        public HorizontalAlignment HorizontalAlignment;
        public VerticalAlignment VerticalAlignment;

        public TextAlignment() {
            this.Size = Vector.Create();
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
