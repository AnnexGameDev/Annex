using System;

namespace Annex.Data.Shared
{
    [Serializable]
    public class IntRect
    {
        public Int Top { get; set; }
        public Int Left { get; set; }
        public Int Width { get; set; }
        public Int Height { get; set; }

        public IntRect() : this(0, 0, 0, 0) {
        }

        public IntRect(Int top, Int left, Int width, Int height) {
            this.Top = top;
            this.Left = left;
            this.Width = width;
            this.Height = height;
        }

        public static implicit operator SFML.Graphics.IntRect(IntRect source) {
            return new SFML.Graphics.IntRect(source.Left.Value, source.Top.Value, source.Width.Value, source.Height.Value);
        }
    }
}
