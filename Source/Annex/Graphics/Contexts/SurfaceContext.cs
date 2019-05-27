using Annex.Data;
using Annex.Data.Binding;

namespace Annex.Graphics.Contexts
{
    public class SurfaceContext
    {
        public PString SourceSurfaceName { get; set; }
        public PVector RenderPosition { get; set; }
        public PVector RenderSize { get; set; }
        public IntRect? SourceSurfaceRect { get; set; }
        public RGBA? RenderColor { get; set; }
        public float Rotation { get; set; }
        public PVector RelativeRotationOrigin { get; set; }
        public bool IsAbsolute { get; set; }

        public SurfaceContext(PString surfaceName) {
            this.SourceSurfaceName = surfaceName;
            this.RenderPosition = new PVector();
            this.RenderSize = new PVector();
            this.SourceSurfaceRect = null;
            this.Rotation = 0;
            this.IsAbsolute = true;
            this.RenderColor = null;
            this.RelativeRotationOrigin = new PVector();
        }
    }
}
