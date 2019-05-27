namespace Annex.Data.Binding
{
    public class IntRect
    {
        public int Top { get; set; }
        public int Left { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public int Right {
            get {
                return this.Left + this.Width;
            }
            set {
                this.Width = value - this.Left;
            }
        }
        public int Bottom {
            get {
                return this.Top + this.Height;
            }
            set {
                this.Height = value - this.Top;
            }
        }

        public IntRect(int top, int left, int width, int height) {
            this.Top = top;
            this.Left = left;
            this.Width = width;
            this.Height = height;
        }

        public IntRect() : this(0, 0, 0, 0) {

        }

        public static implicit operator SFML.Graphics.IntRect(IntRect source) {
            return new SFML.Graphics.IntRect(source.Left, source.Top, source.Width, source.Height);
        }
    }
}
