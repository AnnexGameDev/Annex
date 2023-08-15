using System;
using System.Linq;

namespace Annex_Old.Data
{
    public class RGBA
    {
        public static RGBA White => new RGBA(255, 255, 255);
        public static RGBA Black => new RGBA(0, 0, 0);
        public static RGBA Red => new RGBA(255, 0, 0);
        public static RGBA Green => new RGBA(0, 255, 0);
        public static RGBA Blue => new RGBA(0, 0, 255);
        public static RGBA Yellow => new RGBA(255, 255, 0);
        public static RGBA Purple => new RGBA(200, 0, 200);
        public static RGBA Transparent => new RGBA(0, 0, 0, 0);

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

        public RGBA(byte r, byte g, byte b) : this(r, g, b, Byte.MaxValue) {

        }

        public void Set(RGBA color) {
            this.Set(color.R, color.G, color.B, color.A);
        }

        public void Set(byte r, byte g, byte b) {
            this.Set(r, g, b, 255);
        }

        public void Set(byte r, byte g, byte b, byte a) {
            this.R = r;
            this.G = g;
            this.B = b;
            this.A = a;
        }

        public RGBA() : this(0, 0, 0) {

        }

        public static implicit operator SFML.Graphics.Color(RGBA source) {
            return new SFML.Graphics.Color(source.R, source.G, source.B, source.A);
        }

        public static RGBA Parse(string color) {
            switch (color.ToLowerInvariant()) {
                case "white":
                    return White;
                case "black":
                    return Black;
                case "red":
                    return Red;
                case "green":
                    return Green;
                case "blue":
                    return Blue;
                case "yellow":
                    return Yellow;
                case "purple":
                    return Purple;
                case "transparent":
                    return Transparent;
            }

            var data = color.Split(",").Select(val => byte.Parse(val)).ToArray();
            Debug.Assert(data.Length >= 3, "Color must be a known color, RGB, or RGBA");
            Debug.Assert(data.Length <= 4, "Color must be a known color, RGB, or RGBA");

            return new RGBA(data[0], data[1], data[2], data.Length == 4 ? data[3] : byte.MaxValue);
        }
    }
}
