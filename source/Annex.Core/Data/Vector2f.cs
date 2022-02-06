using System.Diagnostics;

namespace Annex.Core.Data
{
    [DebuggerDisplay("X:{X} Y:{Y}")]
    public class Vector2f
    {
        public float X { get; set; }
        public float Y { get; set; }
    }
}