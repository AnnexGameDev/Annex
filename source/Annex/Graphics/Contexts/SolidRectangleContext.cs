#nullable enable
using Annex.Data;
using Annex.Data.Shared;

namespace Annex.Graphics.Contexts
{
    public class SolidRectangleContext : DrawingContext
    {
        public Vector RenderPosition { get; set; }
        public Vector RenderSize { get; set; }
        public RGBA RenderFillColor { get; set; }
        public RGBA? RenderBorderColor { get; set; }
        public float RenderBorderSize { get; set; }

        public SolidRectangleContext(RGBA color) {
            this.RenderFillColor = color;
            this.RenderPosition = Vector.Create(0, 0);
            this.RenderSize = Vector.Create(100, 100);
            this.RenderBorderColor = null;
            this.RenderBorderSize = 1;
        }
    }
}
