using Annex_Old.Core.Data;

namespace Annex_Old.Core.Graphics.Contexts
{
    public class SolidRectangleContext : DrawContext
    {
        public RGBA FillColor { get; }
        public IVector2<float> Position { get; }
        public IVector2<float> Size { get; }
        public IVector2<float>? RenderOffset { get; init; }
        public Shared<float>? Rotation { get; init; }
        public RGBA? BorderColor { get; init; }
        public Shared<float>? BorderThickness { get; init; }

        public SolidRectangleContext(RGBA color, IVector2<float> position, IVector2<float> size) {
            this.FillColor = color;
            this.Position = position;
            this.Size = size;

            this.RenderOffset = null;
            this.Rotation = null;
            this.BorderColor = null;
            this.BorderThickness = null;
        }
    }
}
