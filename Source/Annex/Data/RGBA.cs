namespace Annex.Data
{
    public class RGBA
    {
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }
        public byte A { get; set; }

        public RGBA(byte r, byte g, byte b, byte a) {
            this.R = r;
            this.G = g;
            this.B = b;
            this.A = a;
        }

        public RGBA(byte r, byte g, byte b) : this(r, g, b, byte.MaxValue) {

        }

        public RGBA() : this(0, 0, 0) {

        }

        public static implicit operator SFML.Graphics.Color(RGBA source) {
            return new SFML.Graphics.Color(source.R, source.G, source.B, source.A);
        }
    }
}
