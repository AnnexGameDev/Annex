using Annex.Data;
using Annex.Data.Shared;

namespace Annex.Graphics.Contexts
{
    public class SurfaceContext
    {
        public String SourceSurfaceName { get; set; }
        public Vector RenderPosition { get; set; }
        public Vector RenderSize { get; set; }
        public IntRect? SourceSurfaceRect { get; set; }
        public RGBA? RenderColor { get; set; }
        public float Rotation { get; set; }
        public Vector RelativeRotationOrigin { get; set; }
        public bool UseUIView { get; set; }

        public SurfaceContext(String surfaceName) {
            this.SourceSurfaceName = surfaceName;
            this.RenderPosition = new Vector();
            this.RenderSize = new Vector();
            this.SourceSurfaceRect = null;
            this.Rotation = 0;
            this.UseUIView = false;
            this.RenderColor = null;
            this.RelativeRotationOrigin = new Vector();
        }
    }
}
