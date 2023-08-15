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

        public static RGBA Parse(string arg) {
            // Maybe it's a name
            if (Enum.TryParse<KnownColor>(arg.ToCamelCaseWord(), out var color)) {
                return new RGBA((uint)color);
            }

            // Maybe it's RGB?
            var colorData = arg.Split(",").Select(val => val.Trim());

            if (colorData.Count() == 3) {
                var byteColorData = colorData.Select(val => byte.Parse(val)).ToArray();
                return new RGBA(byteColorData[0], byteColorData[1], byteColorData[2]);
            }

            if (colorData.Count() == 4) {
                var byteColorData = colorData.Select(val => byte.Parse(val)).ToArray();
                return new RGBA(byteColorData[0], byteColorData[1], byteColorData[2], byteColorData[3]);
            }

            throw new ArgumentException($"Unable to convert to color: {arg}");
        }

        public void Set(RGBA value) {
            this.R = value.R;
            this.G = value.G;
            this.B = value.B;
            this.A = value.A;
        }

        public static implicit operator RGBA(KnownColor knownColor) {
            return new RGBA((uint)knownColor);
        }
    }
}