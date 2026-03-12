namespace Annex.Core.Data;

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

    public RGBA(KnownColor color) : this((uint)color)
    {
    }

    public static RGBA Parse(string arg) {
        // Maybe it's a name
        if (Enum.TryParse<KnownColor>(arg.ToCamelCaseWord(), out var color))
        {
            return new RGBA((uint)color);
        }

        if (arg.StartsWith('#'))
        {
            byte a = 255;

            if (arg.Length == 9)
            {
                a = Convert.ToByte(arg[7..9], 16);
            }

            byte r = Convert.ToByte(arg[1..3], 16);
            byte g = Convert.ToByte(arg[3..5], 16);
            byte b = Convert.ToByte(arg[5..7], 16);
            return new RGBA(r, g, b, a);
        }

        // Maybe it's RGB?
        var colorData = arg.Split(",").Select(val => val.Trim());

        if (colorData.Count() == 3)
        {
            var byteColorData = colorData.Select(val => byte.Parse(val)).ToArray();
            return new RGBA(byteColorData[0], byteColorData[1], byteColorData[2]);
        }

        if (colorData.Count() == 4)
        {
            var byteColorData = colorData.Select(val => byte.Parse(val)).ToArray();
            return new RGBA(byteColorData[0], byteColorData[1], byteColorData[2], byteColorData[3]);
        }

        throw new ArgumentException($"Unable to convert to color: {arg}");
    }

    public void Set(RGBA value) {
        R = value.R;
        G = value.G;
        B = value.B;
        A = value.A;
    }

    public void Set(byte r, byte g, byte b)
    {
        R = r;
        G = g;
        B = b;
        A = 255;
    }

    public void Set(byte r, byte g, byte b, byte a)
    {
        R = r;
        G = g;
        B = b;
        A = a;
    }

    public static implicit operator RGBA(KnownColor knownColor) {
        return new RGBA((uint)knownColor);
    }

    public override string ToString()
    {
        return $"R:{R} G:{G} B:{B} A:{A}";
    }

    public string ToHex()
    {
        return $"#{R:X2}{G:X2}{B:X2}";
    }
}