using System.Diagnostics;

namespace Annex.Core.Data
{
    [DebuggerDisplay("Top:{Top} Left:{Left} Width:{Width} Height:{Height}")]
    public class FloatRect
    {
        public float Top { get; set; }
        public float Left { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }

        public FloatRect() : this(0, 0, 0, 0)
        {
        }

        public FloatRect(float top, float left, float width, float height)
        {
            Top = top;
            Left = left;
            Width = width;
            Height = height;
        }

        public void Set(float top, float left, float width, float height)
        {
            Top = top;
            Left = left;
            Width = width;
            Height = height;
        }

        public void Set(FloatRect floatRect)
        {
            Top = floatRect.Top;
            Left = floatRect.Left;
            Width = floatRect.Width;
            Height = floatRect.Height;
        }
    }
}
