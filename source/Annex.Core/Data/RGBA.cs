namespace Annex.Core.Data
{
    public class RGBA
    {
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }
        public byte A { get; set; }

        public RGBA(uint color) {
            var colorData = BitConverter.GetBytes(color);

            R = colorData[3];
            G = colorData[2];
            B = colorData[1];
            A = colorData[0];
        }

        public RGBA() : this(0, 0, 0) {

        }

        public RGBA(byte r, byte g, byte b) : this(r, g, b, byte.MaxValue) {

        }

        public RGBA(byte r, byte g, byte b, byte a) {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public static RGBA Parse(string colorName) {
            if (Enum.TryParse<KnownColor>(colorName, out var color)) {
                new RGBA((uint)color);
            }
            throw new ArgumentException($"No color exists with the name: {colorName}");
        }

        public static implicit operator RGBA(KnownColor knownColor) {
            return new RGBA((uint)knownColor);
        }
    }
}