using System.Diagnostics;

namespace Annex.Core.Data
{
    [DebuggerDisplay("X:{X} Y:{Y}")]
    public class Vector2i
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Vector2i() : this(0, 0) {

        }

        public Vector2i(int x, int y) {
            this.X = x;
            this.Y = y;
        }
    }
}