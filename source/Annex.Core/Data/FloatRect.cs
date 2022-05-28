using System.Diagnostics;

namespace Annex_Old.Core.Data
{
    [DebuggerDisplay("Top:{Top.Value} Left:{Left.Value} Width:{Width.Value} Height:{Height.Value}")]
    public class FloatRect
    {
        public float Top { get; set; }
        public float Left { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }

        public FloatRect() : this(0, 0, 0, 0) {
        }

        public FloatRect(float top, float left, float width, float height) {
            this.Top = top;
            this.Left = left;
            this.Width = width;
            this.Height = height;
        }

        public void Set(float top, float left, float width, float height) {
            this.Top = top;
            this.Left = left;
            this.Width = width;
            this.Height = height;
        }
    }
}
