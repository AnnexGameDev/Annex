using System.Diagnostics;

namespace Annex.Core.Data
{
    [DebuggerDisplay("X:{X} Y:{Y}")]
    public class Vector2ui
    {
        public uint X { get; set; }
        public uint Y { get; set; }

        public Vector2ui() : this(0, 0) {

        }

        public Vector2ui(uint x, uint y) {
            this.X = x;
            this.Y = y;
        }
    }
}