using System.Diagnostics;

namespace Annex_Old.Core.Data
{
    [DebuggerDisplay("Top:{Top.Value} Left:{Left.Value} Width:{Width.Value} Height:{Height.Value}")]
    public class IntRect
    {
        public int Left { get; set; }
        public int Top { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public IntRect() {

        }

        public IntRect(int top, int left, int width, int height) {
            this.Top = top;
            this.Left = left;
            this.Width = width;
            this.Height = height;
        }

        public void Set(int top, int left, int width, int height) {
            this.Top = top;
            this.Left = left;
            this.Width = width;
            this.Height = height;
        }
    }
}